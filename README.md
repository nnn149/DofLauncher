# DofLauncher
dof台服登录器源码

使用.net4.5  WPF



将resource目录的 `libeay32.dll` `ssleay32.dll` 和 `DofLauncher.exe` 放在一起。

openssl生成rsa

私钥生成 `openssl genrsa -out rsa_2048_priv.pem 2048`
公钥生成 `openssl rsa -pubout -in rsa_2048_priv.pem -out publickey.pem`

相关链接:  https://github.com/openssl-net/openssl-net

