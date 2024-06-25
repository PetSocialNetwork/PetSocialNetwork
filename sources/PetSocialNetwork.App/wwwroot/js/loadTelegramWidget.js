window.loadTelegramWidget = function (dotnetHelper) {
    window.TelegramLoginCallback = function (data) {
        var authData = Object.keys(data).map(key => key + "=" + data[key]).join("&");
        window.DotNet.invokeMethodAsync("HandleAuth", authData);
    };

    const script = document.createElement("script");
    script.async = true;
    script.src = "https://telegram.org/js/telegram-widget.js?4";
    script.setAttribute("data-telegram-login", "YourBotUsername");
    script.setAttribute("data-size", "large");
    script.setAttribute("data-userpic", "true");
    script.setAttribute("data-auth-url", "javascript:TelegramLoginCallback(arguments[0]);");
    script.setAttribute("data-request-access", "write");
    document.getElementById("telegram-login").appendChild(script);
}
