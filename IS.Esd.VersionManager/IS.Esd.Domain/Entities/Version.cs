using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Domain.Entities
{
    public class Version : Entity
    {
        public Guid ProductId { get; set; }

        public DateTime UpdateTime { get; set; }

        public string CurrentVersion { get; set; }

        public Guid Updater { get; set; }

        public string Remark { get; set; }

        public string ClientSourcePath { get; set; }

        public string ClientPackagePath { get; set; }

        public string ServerSourcePath { get; set; }

        public string ServerPackagePath { get; set; }

        public string CollectorSourcePath { get; set; }

        public string CollectorPackagePath { get; set; }

        public string AnalaySourcePath { get; set; }

        public string AnalayPackagePath { get; set; }
    }
}
