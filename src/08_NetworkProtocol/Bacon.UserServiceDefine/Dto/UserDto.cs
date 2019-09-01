using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bacon.ServiceDefine1.Dto
{
    [MessagePackObject]
    public class UserDto
    {
        [Key(0)]
        public int Id { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        [Key(1)]
        public string Account { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Key(2)]
        public string RealName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Key(3)]
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Key(4)]
        public string HeadIcon { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Key(5)]
        public bool? Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [Key(6)]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Key(7)]
        public string MobilePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Key(8)]
        public string Email { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [Key(9)]
        public string WeChat { get; set; }
        /// <summary>
        /// 管理员id
        /// </summary>
        [Key(10)]
        public string ManagerId { get; set; }
        /// <summary>
        /// 安全等级
        /// </summary>
        [Key(11)]
        public int? SecurityLevel { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [Key(12)]
        public string Signature { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        [Key(13)]
        public string OrganizeId { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        [Key(14)]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        [Key(15)]
        public string RoleId { get; set; }
        /// <summary>
        /// 责任人id
        /// </summary>
        [Key(16)]
        public string DutyId { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        [Key(17)]
        public bool? IsAdministrator { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Key(18)]
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        [Key(19)]
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 禁用标记
        /// </summary>
        [Key(20)]
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Key(21)]
        public string Description { get; set; }
    }
}
