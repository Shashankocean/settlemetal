using Xamarin.Forms;

namespace settleMetal
{
    class LocalDB
    {
        static DatabaseQuery database;
        public static DatabaseQuery db_connection
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseQuery(DependencyService.Get<IFileHelper>().GetLocalFilePath("settlemetal.db"));
                }
                return database;
            }
        }
    }
}
