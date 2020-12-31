using System.Collections.Generic;

namespace QueryWeather.Models
{
    public class AliCoinRequestDto
    {
        /// <summary>
        /// 会话Id，session内的对话此ID相同
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 进入意图时用户所说的语句
        /// </summary>
        public string Utterance { get; set; }

        /// <summary>
        /// 本次请求的Id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 请求附带参数
        /// 使用天猫精灵音箱调用技能时额外携带的信息
        /// 在线测试此无数据
        /// </summary>
        public RequestData RequestData { get; set; }

        /// <summary>
        /// 技能配置 OAuth2.0 授权并且用户登录授权账号后可以得到此 token
        /// 详细请查看【OAuth2.0配置文档】
        /// 在线测试无此数据
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 应用Id
        /// 标识用户所使用的的天猫精灵设备的种类
        /// </summary>
        public long BotId { get; set; }

        /// <summary>
        /// 领域Id
        /// </summary>
        public long DomainId { get; set; }

        /// <summary>
        /// 技能Id
        /// </summary>
        public long SkillId { get; set; }

        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        /// 意图Id
        /// </summary>
        public long IntentId { get; set; }

        /// <summary>
        /// 意图标识
        /// </summary>
        public string IntentName { get; set; }

        /// <summary>
        /// 从用户语句中抽取出的 slot 参数信息
        /// </summary>
        public List<SlotEntity> SlotEntities { get; set; }

        /// <summary>
        /// 上一轮状态标识 resultType: SELECT时，用户所做选择的索引值
        /// </summary>
        public List<int> SelectIndexList { get; set; }

        /// <summary>
        /// 上一轮状态标识 resultType: CONFIRM 时，用户所进行的确定（CONFIRMED）或否定（DENIED）回答
        /// </summary>
        public string confirmStatus { get; set; }

        /// <summary>
        /// 用户的设备信息。在线测试没有设备，不会携带设备数据
        /// </summary>
        public object Device { get; set; }

        /// <summary>
        /// 意图标识
        /// </summary>
        public SkillSession SkillSession { get; set; }
    }

    public class RequestData
    {
        /// <summary>
        /// 此技能中用户唯一标识	
        /// </summary>
        public string UserOpenId { get; set; }

        /// <summary>
        /// 此技能中天猫精灵设备唯一标识	
        /// </summary>
        public string DeviceOpenId { get; set; }

        /// <summary>
        /// 天猫精灵设备所处的城市	
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 用户使用带屏设备的标识: "screenStatus": "online"
        /// 若用户使用的是无屏设备，则没有此条数据
        /// 需要到 权限包管理 中申请“设备有无屏特性”权限包
        /// </summary>
        public string ScreenStatus { get; set; }
    }

    public class SlotEntity
    {
        /// <summary>
        /// 意图参数Id
        /// </summary>
        public long IntentParameterId { get; set; }

        /// <summary>
        /// 意图参数名
        /// </summary>
        public string IntentParameterName { get; set; }

        /// <summary>
        /// 原始句子中抽取出来的未做处理的 slot 值
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// slot 归一化后的值
        /// </summary>
        public string StandardValue { get; set; }

        /// <summary>
        /// 该 slot 已存在的会话轮数
        /// </summary>
        public int LiveTime { get; set; }

        /// <summary>
        /// 该 slot 产生时的时间戳
        /// </summary>
        public long CreateTimeStamp { get; set; }
    }

    public class SkillSession
    {
        /// <summary>
        /// 技能粒度session的Id
        /// </summary>
        public string SkillSessionId { get; set; }

        /// <summary>
        /// 用户首次进入技能时：true
        /// 用户后续在技能中对话：false
        /// 如果技能交互中用户有跳出技能，再次进入技能时：true
        /// </summary>
        public bool NewSession { get; set; }
    }
}
