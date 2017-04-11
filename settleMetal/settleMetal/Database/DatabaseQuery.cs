using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace settleMetal
{
    class DatabaseQuery
    {
        readonly SQLiteAsyncConnection connection;

        public DatabaseQuery(string db_path)
        {
            connection = new SQLiteAsyncConnection(db_path);
            connection.CreateTableAsync<User>().Wait();
        }
        public Task<List<User>> get_user()
        {
            connection.Table<User>();
            return connection.QueryAsync<User>("select * from User");
        }
        public Task<int> updateUser(User user)
        {
            return connection.InsertAsync(user);
        }
    }
}
