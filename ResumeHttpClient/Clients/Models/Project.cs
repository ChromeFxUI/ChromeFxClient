using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient.DataAnnotations;

namespace ResumeHttpClient.Clients.Models
{
    public class Project
    {
        [AliasAs("Engine")]
        public string Engine { get; set; }

        [AliasAs("ProjectName")]
        public string ProjectName { get; set; }

        [AliasAs("Description")]
        public string Description { get; set; }

        [AliasAs("Version")]
        public string Version { get; set; }

        [AliasAs("Versions")]
        public List<string> Versions { get; set; }

        [AliasAs("RelativePath")]
        public string RelativePath { get; set; }

        [AliasAs("Id")]
        public string Id { get; set; }

    }
}
