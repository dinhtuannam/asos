namespace Identity.API.Constants;

public static class OTPConstant
{
	public static int Minute { get; } = 5;
	public static string Email { get; } = "namdinhtuan5@gmail.com";
	public static string Key { get; } = "dhxe drjt okgr fdhb";
	public static string RegisterType { get; } = nameof(RegisterType);
	public static string ForgetPasswordType { get; } = nameof(ForgetPasswordType);

	public static DateTime EmailValidTo()
	{
		return DateTime.Now.AddMinutes(Minute);
	}

	public static string EmailTemplate(string code)
	{
		var verifyUrl = $"{Program.BaseUrl}/api/Auth/verify-email/{code}";
		string htmlContent = $@"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Xác Nhận Mã</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f9f9f9;
                        padding: 20px;
                    }}
                    .container {{
                        background-color: #ffffff;
                        border-radius: 5px;
                        padding: 20px;
                        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                    }}
                    .code {{
                        font-size: 24px;
                        font-weight: bold;
                        color: #4CAF50;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Xác Nhận Mã</h2>
                    <p>Chào bạn,</p>
                    <p>Để xác nhận tài khoản của bạn, hãy sử dụng mã xác nhận bên dưới:</p>
                    <p class='code'>{code}</p>
                    <p>Mã này sẽ hết hạn trong <strong>{Minute}</strong> phút. Vui lòng không chia sẻ mã này với bất kỳ ai.</p>
                    <p>Hoặc bạn có thể truy cập đường link sau để xác thực:</p>
                    <p><strong>{verifyUrl}</strong></p>
                    <p>Cảm ơn bạn!</p>
                </div>
            </body>
            </html>
            ";

		return htmlContent;
	}

	public static string ForgetPasswordTemplate(string code)
	{
		var verifyUrl = $"{Program.BaseUrl}/api/Auth/verify-forget-password/{code}";
		string htmlContent = $@"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Mã xác nhận quên mật khẩu</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f9f9f9;
                        padding: 20px;
                    }}
                    .container {{
                        background-color: #ffffff;
                        border-radius: 5px;
                        padding: 20px;
                        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                    }}
                    .code {{
                        font-size: 24px;
                        font-weight: bold;
                        color: #4CAF50;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Mã xác nhận QUÊN MẬT KHẨU</h2>
                    <p>Chào bạn,</p>
                    <p>Để xác nhận quên mật khẩu, bạn hãy sử dụng mã xác nhận bên dưới:</p>
                    <p class='code'>{code}</p>
                    <p>Mã này sẽ hết hạn trong <strong>{Minute}</strong> phút. Vui lòng không chia sẻ mã này với bất kỳ ai.</p>
                    <p>Hoặc bạn có thể truy cập đường link sau để xác thực:</p>
                    <p><strong>{verifyUrl}</strong></p>
                    <p>Cảm ơn bạn!</p>
                </div>
            </body>
            </html>
            ";

		return htmlContent;
	}
}
