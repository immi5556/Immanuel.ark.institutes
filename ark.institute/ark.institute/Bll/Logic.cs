using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ark.institute
{
    public class Logic
    {
        Func<DateTime?, string> GetCurrentTimeStamp = (DateTime? date) => (DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString());
        public List<dynamic> PersistImage(Microsoft.AspNetCore.Http.IFormFileCollection files)
        {
            if (files == null || files.Count == 0) throw new ApplicationException("no files send to upload");
            List<dynamic> lst = new List<dynamic>();
            using (var scope = ServiceActivator.GetScope())
            {
                UploadConfig uplConfig = (UploadConfig)scope.ServiceProvider.GetService(typeof(UploadConfig));
                string dirpath = GetDirectory(System.IO.Path.Combine(uplConfig.FullPath, GetCurrentTimeStamp(null)));
                files.ToList().ForEach(v =>
                {
                    var filepath = GetFile(System.IO.Path.Combine(dirpath, v.FileName));
                    using (var stream = new System.IO.FileStream(filepath, System.IO.FileMode.Create))
                        v.CopyTo(stream);
                    lst.Add(new
                    {
                        FileName = System.IO.Path.GetFileName(filepath),
                        DownloadUrl = $"/Upload/{System.IO.Path.GetFileName(dirpath)}/{System.IO.Path.GetFileName(filepath)}"
                    }); ;
                });
            }
            return lst;
        }
        public string GetDirectory(string dirpath)
        {
            if (!Directory.Exists(dirpath))
                Directory.CreateDirectory(dirpath);
            return dirpath;
        }
        public string GetFile(string fullfilepath, int? seq = null)
        {
            string i = seq.HasValue ? "_" + seq.ToString() : "";
            string fpath1 = Path.Combine(Path.GetDirectoryName(fullfilepath), Path.GetFileNameWithoutExtension(fullfilepath) + i.ToString() + Path.GetExtension(fullfilepath));
            if (!File.Exists(fpath1))
            {
                return fpath1;
            }
            else
            {
                seq = seq.HasValue ? ++seq : 0;
                return GetFile(fullfilepath, seq);
            }
        }
    }
    public static class ServiceActivator
    {
        internal static IServiceProvider _serviceProvider = null;
        public static void UseArkUpload(this IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices;
        }
        public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
        {
            var provider = serviceProvider ?? _serviceProvider;
            return provider?
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
        }
    }
    public static class ExtnCls
    {
        public static void AddArkUpload(this IServiceCollection service)
        {
            service.AddArkUpload(new UploadConfig() { FullPath = System.IO.Path.Combine(Environment.CurrentDirectory, "wwwroot", "Upload") });
        }
        public static void AddArkUpload(this IServiceCollection service, UploadConfig config)
        {
            service.AddSingleton<UploadConfig>(config);
        }
        public static void AddArkUpload(this IServiceCollection service, string fullpath)
        {
            service.AddArkUpload(new UploadConfig() { FullPath = fullpath });
        }
        public static void AddArkUpload(this IServiceCollection service, IWebHostEnvironment env)
        {
            service.AddArkUpload(new UploadConfig() { FullPath = System.IO.Path.Combine(env.WebRootPath, "Upload") });
        }
    }
    public class UploadConfig
    {
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
    }
}