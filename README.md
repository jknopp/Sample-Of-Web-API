# Sample Of Web API
What will you find in this sample of web API ? Here is the answers ;

 - **IoC Container** ; in this project ApiControllers resolve in container.( Castle Windsor )
 - **Logging Mechanism** ; in this project we're using Log4Net for logging mechanism and all requests have Unique track Id ( for c# its Guid )
 - **Unit Tests** ; in this project we're using Microsoft's Unit test framework.
 - **Global Exception Logging And Handling** ; in this project we have 2 services for exception logging and handlig.These services are inherited **IExceptionLogger** and **IExceptionHandler** interfaces.
 - **Swagger Configuration** ; Documentation for Swagger.
 - **Global Request Logging** ; Global logging Attribute.
 
## Dependency Injection For API
This sample project , we will use **Castle Windsor** for IoC Container.You can use another IoC Container framework (for example : Unity , Ninject etc. )

Web API provides two primary mechanism for controller dependency injection. Implementing a custom **IHttpControllerActivator** and implementing a custom **IDependencyResolver**. ([*ASP. NET Web API 2 Recipes: A Problem-Solution Approach - Filip Wojcieszyn*](https://books.google.com.tr/books?id=7aE8BAAAQBAJ&printsec=frontcover&hl=tr#v=onepage&q&f=false))

In this sample, using the **IHttpControllerActivator**. It's resolve controller classes.

**For Example to IHttpControllerActivator Implentation;**

```c#
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer _container;
        public WindsorCompositionRoot(IWindsorContainer container)
        {
            _container = container;
        }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller =
            (IHttpController)this._container.Resolve(controllerType);

           request.RegisterForDispose(
            new Release(
                () => this._container.Release(controller)));

            return controller;
        }
    }
```
**Here is Controller Installer ;**
```c#
public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                        .BasedOn<ApiController>()
                        .LifestylePerWebRequest()
                        );
        }
    }
``` 
**And Global.asax configuration ;**
```c#
IWindsorContainer container = new WindsorContainer();
container.Install(FromAssembly.This());
GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container)); 
``` 

After this implentation , all apicontroller release this Activator class and **container** is carrying all controller.

## Logging Mechanism

**Log4net** is a usefull component for all project because it's easy to configuration and simple to use.

Download the log4net package with nugget.And download *[my other project](https://github.com/mfarkan/Log4NetCustomLogger)* to use with it :)

## Unit Tests For API

I used this guide for Unit testing ; 

[Microsoft Documentation](https://docs.microsoft.com/tr-tr/aspnet/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api)

Here is the TestControllerClass;
```c#
[TestClass]
    public class TestProductController
    {
        [TestMethod]
        public void getProduct_ReturnEmptyProduct()
        {
            var service = new ProductService();
            var controller = new ProductController(service);
            var result = controller.getProduct(9999);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void getProduct_ReturnFirstProduct()
        {
            var service = new ProductService();
            var controller = new ProductController(service);
            Product sample = new Product()
            {
                ProductId = 1,
                ProductName = "MFA",
                Status = 0,
                ResponseCode = null,
                Message = null
            };
            var result = controller.getProduct(1);
		Assert.AreEqual(sample.ProductName, result.ProductName);
        }
```
## Global Exception Logging And Handling
It's come to life with Asp. NET Web API 2.1.

All unhandled exceptions can now be logged through one central mechanism, and the behavior for unhandled exceptions can be customized.([*Global Logging*](https://docs.microsoft.com/en-us/aspnet/web-api/overview/releases/whats-new-in-aspnet-web-api-21))

**When to use** ;

-   Exception loggers are the solution to seeing all unhandled exception caught by Web API.
-  Exception handlers are the solution for customizing all possible responses to unhandled exceptions caught by Web API.

**For example IExceptionLogger** ;
```c#
public class CustomExceptionLogger : ExceptionLogger
    {
        private readonly ILogger _logger;
        public CustomExceptionLogger(ILogger logger)
        {
            this._logger = logger;
        }
        public override void Log(ExceptionLoggerContext context)
        {
            var Guid = context.Request.Headers.GetValues("TrackId").FirstOrDefault();
            _logger.Log("Track Id : " + Guid + " - ERROR : {" + context.Exception.Message + "}", LogType.Error);
            base.Log(context);
        }
    }
```
**For example IExceptionHandling** ;
```c#
public class HandlerLogger :ExceptionHandler
    {
        private readonly ILogger _logger;
        public HandlerLogger(ILogger logger)
        {
            _logger = logger;
        }
        public override void Handle(ExceptionHandlerContext context)
        {
            ResponseHeader header;
            var ReturnType = context.Request.GetActionDescriptor().ReturnType;
            header = Activator.CreateInstance(ReturnType) as ResponseHeader;
            header.Message = context.Exception.Message;
            header.ResponseCode = "0001";
            header.Status = 1;
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, header);
            context.Result = new ResponseMessageResult(response);
            base.Handle(context);
        }
    }
```
**And Global.asax configuration ;**

```c#
GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new HandlerLogger(container.Resolve<ILogger>()));
GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionLogger), new CustomExceptionLogger(container.Resolve<ILogger>()));
```
In these code blocks says handle unexpected error **globally** and **return** custom response and response model.

## Swagger Configuration

When consuming a Web API, understanding its various methods can be challenging for a developer.[*Swagger*](https://swagger.io/), also known as Open API, solves the problem of generating useful documentation and help pages for Web APIs.  It provides benefits such as interactive documentation, client SDK generation, and API discoverability.

## Global Request Logging

The goal is ,who is requesting our API and if its success , What is the response?Im managing it with using **ActionFilterAttribute**.

ActionFilterAttribute has two methods ;

 - **OnActionExecuting** ; Its for using Request logging and create **GuID** add to Request Header.
 - **OnActionExecuted** ; If request successfully end , log the Response.

Here is the **LoggingAttribute** ;
```c#
    public class LoggingAttribute : ActionFilterAttribute
    {
        private readonly ILogger logger;
        private string _Guid;
        public LoggingAttribute(ILogger logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string jSonData = actionExecutedContext.Response?.Content.ReadAsStringAsync().Result;
            this.logger.Log("Track Id : " + _Guid + " - OutGoing Response {" + jSonData + "}", LogType.Debug);
            base.OnActionExecuted(actionExecutedContext);
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string jSonData = actionContext.Request.Content?.ReadAsStringAsync().Result;
            _Guid = Guid.NewGuid().ToString();
            actionContext.Request.Headers.Add("TrackId", _Guid);
            this.logger.Log("Track Id : " + _Guid + " - " + actionContext.Request.RequestUri.LocalPath + " - Incoming Request {" + jSonData + "}", LogType.Debug);
            base.OnActionExecuting(actionContext);
        }
    }
``` 
Global.asax configuration ; 
```c#
GlobalConfiguration.Configuration.Filters.Add(new LoggingAttribute(container.Resolve<ILogger>()));
```

Thank you !

***Resources ;***

 - [*Global Exception Logging*](https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/web-api-global-error-handling)
 - [*New Futures Of Web API 2.1*](https://docs.microsoft.com/en-us/aspnet/web-api/overview/releases/whats-new-in-aspnet-web-api-21)
 - [*Ioc Container Frameworks*](https://www.hanselman.com/blog/ListOfNETDependencyInjectionContainersIOC.aspx)
 - [*Swagger Documentation*](https://swagger.io)
 - [*Castle Windsor*](http://www.castleproject.org)
