using EF_CF_Data.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace EF_CF_Data.Mappings
{
    public class EntityMapping<T> : EntityTypeConfiguration<T>, IEntityMapping where T : Entity
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        protected EntityMapping()
        {
            this.HasKey(m => m.Id);
        }

        /// <summary>
        /// 将DbModelBuilder加入注册
        /// </summary>
        /// <param name="modeBuilder"></param>
        public void Configure(DbModelBuilder modeBuilder)
        {
            modeBuilder.Configurations.Add<T>(this);
        }
    }
}
