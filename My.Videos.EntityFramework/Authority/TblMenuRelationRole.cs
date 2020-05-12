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
    [Table("tblMenuRelationRole")]
    public class TblMenuRelationRole
    {
        private int _roleid;
        private int _menuId;
        private DateTime _createtime;
        private DateTime _updatetime;
        public TblMenuRelationRole()
        {
            _roleid = 0;
            _menuId = 0;
            _createtime = DateTime.Now;
            _updatetime = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [DefaultValue(0)]
        public int RoleId
        {
            get { return _roleid; }
            set { _roleid = value; }
        }
        /// <summary>
        /// 角色姓名
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string RoleName { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        [DefaultValue(0)]
        public int MenuId
        {
            get { return _menuId; }
            set { _menuId = value; }
        }
        /// <summary>
        /// 菜单姓名
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string MenuName { get; set; }

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
