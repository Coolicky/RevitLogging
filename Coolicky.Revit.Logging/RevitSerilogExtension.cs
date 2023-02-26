using System;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Coolicky.Revit.Logging
{
    public static class RevitSerilogExtension
    {
        public static LoggerConfiguration CreateRevitConfiguration(this LoggerConfiguration config,
            UIControlledApplication app, string appName, string appVersion)
        {
            return config.Enrich.WithExceptionDetails()
                .Enrich.WithProperty("machineName", Environment.MachineName)
                .Enrich.WithProperty("userName", Environment.UserName)
                .Enrich.WithProperty("domain", Environment.UserDomainName)
                .Enrich.WithProperty("operatingSystem", Environment.OSVersion)
                .Enrich.WithProperty("revitVersionName", app.ControlledApplication.VersionName)
                .Enrich.WithProperty("revitVersionNo", app.ControlledApplication.VersionNumber)
                .Enrich.WithProperty("revitSubVersionNo", app.ControlledApplication.SubVersionNumber)
                .Enrich.WithProperty("revitVersionBuild", app.ControlledApplication.VersionBuild)
                .Enrich.WithProperty("revitType", app.ControlledApplication.Product.ToString())
                .Enrich.WithProperty("revitLanguage", app.ControlledApplication.Language.ToString())
                .Enrich.WithProperty("app", appName)
                .Enrich.WithProperty("version", appVersion);
        }
        
        public static LoggerConfiguration CreateRevitConfiguration(this LoggerConfiguration config,
            Application app, string appName, string appVersion)
        {
            return config.Enrich.WithExceptionDetails()
                .Enrich.WithProperty("machineName", Environment.MachineName)
                .Enrich.WithProperty("userName", Environment.UserName)
                .Enrich.WithProperty("domain", Environment.UserDomainName)
                .Enrich.WithProperty("operatingSystem", Environment.OSVersion)
                .Enrich.WithProperty("revitVersionName", app.VersionName)
                .Enrich.WithProperty("revitVersionNo", app.VersionNumber)
                .Enrich.WithProperty("revitSubVersionNo", app.SubVersionNumber)
                .Enrich.WithProperty("revitVersionBuild", app.VersionBuild)
                .Enrich.WithProperty("revitType", app.Product.ToString())
                .Enrich.WithProperty("revitLanguage", app.Language.ToString())
                .Enrich.WithProperty("app", appName)
                .Enrich.WithProperty("version", appVersion);
        }

        public static void Error(this ILogger logger, Exception exception, Activity activity)
        {
            logger.Error(exception, "Error Occurred for Activity {@Activity}", activity);
        }

        public static void Activity(this ILogger logger, Activity activity,
            LogEventLevel level = LogEventLevel.Information)
        {
            logger?.Write(level, "Activity: {@Activity}", activity);
        }
    }
}