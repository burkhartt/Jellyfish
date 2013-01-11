using Simple.Data.MongoDB;

namespace Database {
    internal class Database : IDatabase {
        public dynamic GetTheDatabase() {
            return Simple.Data.Database.Opener.OpenMongo("mongodb://localhost:27017/local");
        }
    }
}