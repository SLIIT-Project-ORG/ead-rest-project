internal class MongoDbIdentityConfiguration
{
    public object MongoDbSettings { get; set; }
    public Action<object> IdentityOptionsAction { get; set; }
}