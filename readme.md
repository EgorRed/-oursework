# Launch instructions
create an 
> appsettings.json

file in the **AccountingForExpirationDates** folder

the contents of **appsettings.json**
```sh
{
  "Logging": {
    "LogLevel": {
      "Default": "",
      "Microsoft.AspNetCore": ""
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ConnStr": ""
  },
  "JWT": {
    "ValidAudience": "",
    "ValidIssuer": "",
    "Secret": ""
  }
}
```