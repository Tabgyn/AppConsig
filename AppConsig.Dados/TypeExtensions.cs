using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AppConsig.Dados
{
    public static class TypeExtensions
    {
        private static readonly ConcurrentDictionary<string, IEnumerable<string>> PrimaryKeyNameCache =
            new ConcurrentDictionary<string, IEnumerable<string>>();

        private static readonly ConcurrentDictionary<string, string> ColumnNameCache =
            new ConcurrentDictionary<string, string>();

        private const string ProxyNamespace = @"System.Data.Entity.DynamicProxies";

        public static Type GetEntityType(this Type entityType)
        {
            while (true)
            {
                if (entityType == null || entityType.Namespace != ProxyNamespace) return entityType;

                entityType = entityType.BaseType;
            }
        }

        public static IEnumerable<string> GetPrimaryKeyNames(this Type type, DbContext context)
        {
            var key = type.FullName;
            return GetFromCache(PrimaryKeyNameCache, key, k => PrimaryKeyNamesFactory(type, context));
        }

        public static PropertyInfo GetPropertyInfo<TSource>(Expression<Func<TSource, object>> propertyLambda)
        {
            var type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

            if (propInfo.ReflectedType != null && (type != propInfo.ReflectedType &&
                                                   !type.IsSubclassOf(propInfo.ReflectedType)))
                throw new ArgumentException(
                    $"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");

            return propInfo;
        }

        public static PropertyInfo GetPropertyInfo<TSource, TEntity>(Expression<Func<TSource, TEntity>> propertyLambda)
        {
            Type type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

            if (propInfo.ReflectedType != null && (type != propInfo.ReflectedType &&
                                                   !type.IsSubclassOf(propInfo.ReflectedType)))
                throw new ArgumentException(
                    $"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");

            return propInfo;
        }

        public static string GetPropertyName(this Type type, string propertyName)
        {
            var key = GetFullPropertyName(type, propertyName);
            return GetFromCache(ColumnNameCache, key, k => ColumnNameFactory(type, propertyName));
        }

        private static string GetFullPropertyName(Type type, string propertyName)
        {
            return type.FullName + "." + propertyName;
        }

        private static IEnumerable<string> PrimaryKeyNamesFactory(Type type, IObjectContextAdapter context)
        {
            var objectContext = context.ObjectContext;
            EdmType edmType;

            if (objectContext.MetadataWorkspace.TryGetType(type.Name, type.Namespace, DataSpace.OSpace, out edmType))
            {
                return edmType.MetadataProperties.Where(mp => mp.Name == "KeyMembers")
                    .SelectMany(mp => mp.Value as ReadOnlyMetadataCollection<EdmMember>)
                    .OfType<EdmProperty>().Select(edmProperty => edmProperty.Name);
            }

            throw new Exception($"could not find type '{type.FullName}' from objectContext");
        }

        private static string ColumnNameFactory(this Type type, string propertyName)
        {
            var columnName = propertyName;
            var entityType = type.GetEntityType();
            var columnAttribute = entityType.GetProperty(propertyName).GetCustomAttribute<ColumnAttribute>(false);
            if (!string.IsNullOrEmpty(columnAttribute?.Name))
            {
                columnName = columnAttribute.Name;
            }

            return columnName;
        }

        private static TVal GetFromCache<TKey, TVal>(ConcurrentDictionary<TKey, TVal> dictionary, TKey key,
            Func<TKey, TVal> valueFactory)
        {
            lock (dictionary)
            {
                return dictionary.GetOrAdd(key, valueFactory);
            }
        }
    }
}