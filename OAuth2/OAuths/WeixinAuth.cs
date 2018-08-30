using System;
using System.Collections.Generic;
using System.Text;

namespace OAuth2
{
    public class WeiXinAuth : OAuth2Base
    {
        internal override OAuthServer server => OAuthServer.WeiXin;

        internal override string OAuthUrl => $"https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect";

        internal override string TokenUrl => $"https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code";

        internal override string ImgUrl => "<img align='absmiddle' src=\"/skin/system_tech/images/oauth_wechat.png\" /> 微信";

        internal string UserInfoUrl => $"https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN";

        public override bool Authorize()
        {
            throw new NotImplementedException();
        }
    }
}
