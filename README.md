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
