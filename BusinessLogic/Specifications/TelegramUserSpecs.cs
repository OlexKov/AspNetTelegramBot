using Ardalis.Specification;
using BusinessLogic.Entities;


namespace BusinessLogic.Specifications
{
    internal class TelegramUserSpecs
    {
        public class GetAllUsers : Specification<TelegramUser>
        {
            public GetAllUsers() => Query
                .Where(x=>!x.IsAdmin)
                .AsNoTracking();
        }
        public class GetAllAdmins : Specification<TelegramUser>
        {
            public GetAllAdmins() => Query
                .Where(x => x.IsAdmin)
                .AsNoTracking();
        }

        public class GetById : Specification<TelegramUser>
        {
            public GetById(long id) => Query
                .Where(x => x.Id == id)
                .AsNoTracking();
        }
    }
}
