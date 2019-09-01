using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bacon.ServiceDefine1.Entity
{
    [BsonIgnoreExtraElements]
    public class UserEntity
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public int id { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadIcon { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool? Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 管理员id
        /// </summary>
        public string ManagerId { get; set; }
        /// <summary>
        /// 安全等级
        /// </summary>
        public int? SecurityLevel { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 责任人id
        /// </summary>
        public string DutyId { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool? IsAdministrator { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? DeleteMark { get; set; }
        /// <summary>
        /// 禁用标记
        /// </summary>
        public bool? EnabledMark { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
