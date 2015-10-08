using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class NoticeService : EntityService<Notice>, INoticeService
    {
        public NoticeService(IContext context) 
            : base(context) 
        { 
            Context = context; 
            Dbset = Context.Set<Notice>(); 
        }
    }
}