using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBookOneMedia.Servers.NATO.Build.DesignerCode.GeminiAgentResponce
{
    public class GeminiResponse
    {
        public string Code { get; set; }
        public string NonCodeText { get; set; }

        public GeminiResponse()
        {
            Code = string.Empty;
            NonCodeText = string.Empty;
        }
    }
}
