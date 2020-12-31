using System.Collections.Generic;

namespace QueryWeather.Models
{
    public class AliCoinResponseDto
    {
        /// <summary>
        /// 0默认表示成功，其他不成功的字段自己可以确定
        /// </summary>
        public int ReturnCode { get; set; }

        /// <summary>
        /// 出错时解决办法的描述信息
        /// </summary>
        public string ReturnErrorSolution { get; set; }

        /// <summary>
        /// 返回执行成功的描述信息
        /// </summary>
        public string ReturnMessage { get; set; }

        /// <summary>
        /// 意图理解后的执行结果
        /// </summary>
        public AliCoinResult ReturnValue { get; set; }
    }

    public class AliCoinResult
    {
        /// <summary>
        /// 回复给用户的 TTS 文本信息
        /// </summary>
        public string Reply { get; set; }

        /// <summary>
        /// 回复时的状态标识，这里不再通过resultType的取值决定是否开麦
        /// 枚举值：RESULT、ASK_INF 和 CONFIRM
        /// RESULT：用于正常结束交互对话 ，音箱播放完回复文案后会闭麦。但此时依旧属于此技能的多轮对话中，用户唤醒天猫精灵后仍可以只使用语料进入意图。若用户使用了其它技能或 5 分钟内没有再次交互，就会退出此技能的多轮对话状态，需要使用调用词才能再次进入此技能
        /// ASK_INF：用于追问用户参数信息或引导用户继续对话，期望用户回答本意图或下的参数取值或者本意图或其它意图中的语料，音箱询问后会自动开麦。期望用户回答的参数需要在 askedInfos 字段中指定
        /// CONFIRM：用于向用户确认信息，并期待用户做出肯定或否定回答，音箱询问后会自动开麦。一般用于重要参数的再次确定。若用户做了肯定或否定回答，则请求数据中会携 confirmStatus: CONFIRMED/DENIED 字段。可以通过 confirmParaInfo 字段指定用户表达需要匹配的参数。如果响应数据没有携带 confirmParaInfo数据，或两个参数都没有匹配到，则依靠平台来判断用户肯定或否定的想法。若平台没有判断出用户的想法，则可能会跳出技能
        /// </summary>
        public string ResultType { get; set; }

        /// <summary>
        /// resultType: ASK_INF状态下需要携带追问的参数信息，包括参数名称和意图Id。可同时携带多个参数
        /// </summary>
        public List<AskedInfoMsg> AskedInfos { get; set; }

        /// <summary>
        /// 生成回复语句时携带的额外信息
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }

        /// <summary>
        /// 播控类信息，支持播放音频素材和 TTS 文本
        /// </summary>
        public List<Action> Actions { get; set; }

        /// <summary>
        /// 最新版响应协议定义的command结构
        /// </summary>
        public List<GwCommand> GwCommands { get; set; }

        /// <summary>
        /// resultType: CONFIRM状态下可以携带的匹配用户肯定和否定回答的参数名称
        /// </summary>
        public List<ConfirmParaInfo> ConfirmParaInfo { get; set; }

        /// <summary>
        /// "SUCCESS"代表执行成功
        /// "PARAMS_ERROR"代表接收到的请求参数出错
        /// "EXECUTE_ERROR"代表自身代码有异常
        /// "REPLY_ERROR"代表回复结果生成出错
        /// </summary>
        public string ExecuteCode { get; set; }
    }

    public class AskedInfoMsg
    {
        /// <summary>
        /// 追问的参数名称，此名称是在意图中定义的，不是实体标识
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 参数所在的意图Id，从请求参数中获得，请勿使用固定值
        /// 开发阶段的意图Id 和线上阶段的意图Id不同
        /// </summary>
        public long IntentId { get; set; }
    }

    public class Action
    {
        /// <summary>
        /// 1：根据音频素材Id播放音频素材，Action名称，该名字必须设置为："audioPlayGenieSource"
        /// 2：播放 TTS 文本内容，Action名称，该名字必须设置为"playTts"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 1：根据音频素材Id播放音频素材，Action中的携带信息的集合，必要的key: "audioGenieId"，value为该技能下音频素材库内的音频Id
        /// 2：播放 TTS 文本内容，
        /// key："content"，value为需要播报的内容
        /// key: "format"，value: "text"
        /// key: "showText"，value为设备对话流展示文字，一般与"content"一致即可
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }
    }

    public class GwCommand
    {
        /// <summary>
        /// 指令命名空间
        /// </summary>
        public string CommandDomain { get; set; }

        /// <summary>
        /// 指令名称
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// 指令数据
        /// </summary>
        public Dictionary<string, object> Payload { get; set; }
    }

    public class ConfirmParaInfo
    {
        /// <summary>
        /// 用户表达匹配到此参数，表示确定意思
        /// </summary>
        public string ConfirmParameterName { get; set; }

        /// <summary>
        /// 用户表达匹配到此参数，表示否定意思
        /// </summary>
        public string DenyParameterName { get; set; }
    }
}
