1. This is a Blogger Web Application that implemented in Blazor Web Assembly + Web API + MS SQL Server with Entity Framework Core + Clean Architecture + Repository + Unit Of Work pattern.

This web application use some Nuget Packages as following:

-Microsoft.AspNetCore.Authentication.JwtBearer Version="7.0.12"

-Microsoft.AspNetCore.Components.Authorization Version="7.0.12"

-Microsoft.Extensions.Http Version="7.0.0"

-Microsoft.AspNetCore.Mvc.NewtonsoftJson Version="7.0.12"

-AutoMapper.Extensions.Microsoft.DependencyInjection Version="12.0.1"

-Microsoft.Extensions.DependencyInjection Version="7.0.0"

-SeriLog.AspNetCore Version="7.0.0"

-BCrypt.Net-Next Version="4.0.3"

-Microsoft.EntityFrameworkCore Version="7.0.12"

-Microsoft.EntityFrameworkCore.SqlServer Version="7.0.12"

-Microsoft.EntityFrameworkCore.Tools Version="7.0.12"

-System.IdentityModel.Tokens.Jwt Version="7.0.3" 

-AutoMapper Version="12.0.1"

-Blazored.LocalStorage Version="4.4.0" 

-Blazored.Toast Version="4.1.0"

-Blazored.Modal Version="7.1.0"

-Blazored.TextEditor" Version="1.1.0"

3. Structure of Blogger Application:
   
   ![image](https://github.com/manvominh/Blogger/assets/133474782/1cf4c1b8-303a-4c2e-b64e-3c3947234597)
   
4. Some main functions of Blogger web application:
- There are 2 roles in system: Administrator + User
    * User: can manage user's post including: new + edit + publish owner post and comment on any published post
    * Administrator: including User role + administrate system such as: manage users + roles in Blogger system
- Home page + Details of Post: all anonymous users can see pages: list posts + details of post, information of Post including: Title + Introduction + Image + Body of Post.
- Anonymous user can register + login ( if already had account) to Blogger system.
- Logged user can:
    * modify profile
    * create + edit + comment + publish post
- Administrator can manage users + roles in Blogger system.
5. Video introduce source code + main feature:
  
