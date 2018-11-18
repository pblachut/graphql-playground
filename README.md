# graphql-playground


Simple graphql example which serves users and employees using grapql endpoint.

Users and employees are stored in ravendb.

```csharp
public class Employee
{
    
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
}

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
}
```

`docker-compose.yml` setup ravendb instance. 

`createDbDocuments.http` utilize http endpoint to easy put employees and users into database