# SqlTransientErrorHandler
Simple error handler for SQL Server / SQL Database.

# How to use it:

```
new TransientErrorHandler()
  .WithIncrementalTimeout(true)
  .WithAttempts(3)
  .Execute(() =>
  {
      using (var sqlConnection = new SqlConnection())
      {
          //sql code
      }
  });
  
```
# if you need to return something:
```
return new TransientErrorHandler()
  .WithIncrementalTimeout(true)
  .WithAttempts(3)
  .Execute(() =>
  {
      using (var sqlConnection = new SqlConnection())
      {
          //sql code
      }
  });
```

# for async methods:

```
return await new TransientErrorHandler()
    .WithAttempts(3)
    .WithIncrementalTimeout(true)
    .Execute(async () =>
    {
        using (var sqlConnection = _config.SQLDatabase.GetRawConnection())
        {
            //sql code
        }
    });
```
