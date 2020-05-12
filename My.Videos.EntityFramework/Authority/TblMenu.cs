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
    [Table("tblMenu")]
    public class TblMenu
    {
        private int _delete;
        private int _parentId;
        private DateTime _createtime;
        private DateTime _updatetime;
        public TblMenu()
        {
            _delete = 0;
            _parentId = 0;
            _createtime = DateTime.Now;
            _updatetime = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string MenuURL { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string FontClass { get; set; }

        /// <summary>
        /// 父结点
        /// </summary>
        [DefaultValue(0)]
        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

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

        /// <summary>
        /// 是否删除 1:删除
        /// </summary>
        [DefaultValue(0)]
        public int IsDelete
        {
            get { return _delete; }
            set { _delete = value; }
        }
    }
}
