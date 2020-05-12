using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.EntityFramework.Authority
{
    [Table("tblLog")]
    public class TblLog
    {
        private int _logtype;
        private DateTime? _createtime;

        public TblLog()
        {
            _logtype = 0;
            //_createtime = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(2000)]
        [Column(TypeName = "nvarchar")]
        public string Context { get; set; }

        /// <summary>
        /// 日志类型 1:用户 2:角色 3:菜单 4:菜单用户关联 5:角色菜单关联
        /// </summary>
        [DefaultValue(0)]
        public int LogType
        {
            get { return _logtype; }
            set { _logtype = value; }
        }

        /// <summary>
        /// 类名
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "varchar")]
        public string ClassName { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string ApplicationId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                if (_createtime == null)
                {
                    _createtime = DateTime.Now;
                }
                return _createtime.Value;
            }
            set { _createtime = value; }
        }
    }
}
