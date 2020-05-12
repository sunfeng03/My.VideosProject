using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.EntityFramework.Authority
{
    [Table("tblRole")]
    public class TblRole
    {
        private int _isdelete;
        private DateTime _createtime;
        private DateTime _updatetime;
        public TblRole()
        {
            _isdelete = 0;
            _createtime = DateTime.Now;
            _updatetime = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string RoleName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }

        /// <summary>
        /// 创建姓名
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }

        /// <summary>
        /// 更新人
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string UpdateName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
    }
}
