using Simple.Data.MongoDB;

namespace Web.Database {
    public interface IDatabase {
        dynamic GetTheDatabase();
    }

    public class Database : IDatabase {
        public dynamic GetTheDatabase() {
            return Simple.Data.Database.Opener.OpenMongo("mongodb://localhost:27017/local");
        }
    }
}