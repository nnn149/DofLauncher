# DofLauncher
dof台服登录器源码

使用.net core 3.1  WPFAPP

使用uuid登录，需要数据库注册好账号。

将resource目录的 `libeay32.dll` `ssleay32.dll` 和 `DofLauncher.exe` 放在一起。

openssl生成rsa

私钥生成 `openssl genrsa -out rsa_2048_priv.pem 2048`
公钥生成 `openssl rsa -pubout -in rsa_2048_priv.pem -out publickey.pem`

相关链接:  https://github.com/openssl-net/openssl-net

