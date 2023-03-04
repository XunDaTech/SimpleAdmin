<div align="center"><h1 align="center">SimpleAdmin</a></h1></div>
<div align="center"><h4 align="center">⚡️让开发变得简单,这件事本身就不简单!⚡️</h4></div>

## 🎨框架介绍🎨
💥SimpleAdmin一个小而美的通用业务型后台管理系统。前端基于小诺2.0版本,采用Vue3+Vite+Vuex,并在此基础上增加更人性化功能,后端采用.NET6/7+Furion,ORM采用Sqlsugar单例模式。基于RBAC+多机构的权限管理模式，实现接口级别的数据权限控制，集成国密加解密插件。将日常开发中的业务场景和框架紧密结合，坚持以人为本,以业务为中心，做到开箱即用,代码简洁、易扩展，注释详细，文档齐全，让你的开发少走弯路。

## 🍕系统特色🍕
#### 📗后台基于Furion脚手架
后端基于Furion脚手架搭建。Furion是目前.NET最好用的后端框架之一,有着全网最详细的使用文档和说明，作为新手或二次开发也能很快的上手，你想要的功能基本都能在Furion中找到。
##### 🍎 Furion框架特点

- 全新面貌：基于 `.NET5/6/7+` 平台，没有历史包袱
- 极少依赖：框架只依赖两个第三方包
- 极易入门：只需要一个 `Inject()` 即可完成配置
- 极速开发：内置丰富的企业应用开发功能
- 极其灵活：轻松面对多变复杂的需求
- 极易维护：采用独特的架构思想，只为长久维护设计
- 完整文档：提供完善的开发文档
- **跨全平台：支持所有主流操作系统及 .NET 全部项目类型**

文档地址:[https://dotnetchina.gitee.io/furion](https://dotnetchina.gitee.io/furion)

源码地址:[https://gitee.com/dotnetchina/Furion](https://gitee.com/dotnetchina/Furion)

#### 🍭ORM基于Sqlsugar
SqlSugar是一款老牌.NET开源ORM框架，由果糖大数据科技团队维护和更新 ，开箱即用
最易上手的ORM框架，本系统也是基于Sqlsugar单例模式+CodeFirst+仓储的结构，无需担心作用域问题，直接爽撸！

文档地址:[https://www.donet5.com/Home/Doc](https://www.donet5.com/Home/Doc)

源码地址:[https://gitee.com/dotnetchina/SqlSugar](https://gitee.com/dotnetchina/SqlSugar)

#### 📘基于Redis的分布式缓存
本系统使用了大量的缓存操作，一些基础配置和用户权限信息都放在了缓存中，用户首次登录后，下次再登录接口耗时实测`10-30ms`。

使用了Redis作为分布式缓存，客户端使用的是基于[NewLife.Redis](https://github.com/NewLifeX/NewLife.Redis)二次封装的[SimpleRedis](https://gitee.com/zxzyjs/SimpleRedis.git)

##### 🍎特性
* 在ZTO大数据实时计算广泛应用，200多个Redis实例稳定工作一年多，每天处理近1亿包裹数据，日均调用量80亿次
* 低延迟，Get/Set操作平均耗时200~600us（含往返网络通信）
* 大吞吐，自带连接池，最大支持1000并发 
* 高性能，支持二进制序列化
  
#### 📞支持Mqtt/Signalr的即时通讯
作为前后端分离项目,前后端交互是一个非常重要的功能。目前主流框架都是通过Socket实现，本系统自然也是实现了基于`Signalr`的前后端交互，并在此基础上实现了基于`MQTT`的前后端交互功能，MQTT相比socket业务场景更多更灵活，在物联网方向有着非常多的应用。.NET应用在工业物联网方向也是有很多的，学习MQTT还是非常有必要的。

#### 🧱接口级别的数据权限
权限设计作为一个管理系统的灵魂，是一个系统好不好用的关键。本系统是基于RBAC+多机构的权限管理模式，并实现了接口级别的数据权限，可以指定某个角色的某个接口的数据权限，非常的灵活。
<img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/接口数据权限.png"/>

在该系统中，获取数据权限只需一个方法搞定。
```cs
//获取数据范围
var dataScope = await _sysUserService.GetLoginUserApiDataScope();
```
#### 🔧代码生成器
大部分项目里，其实有很多代码是重复的，几乎每个模块都有 CRUD 增删改查的功能，而这些功能的实现代码往往是大同小异的。如果这些功能都要自己去手写，非常无聊枯燥，浪费时间且效率很低，还可能会写错。代码生成功能通过选择数据库表，完成单表的增删改查管理功能，可以生成包括前端、后端、和SQL语句，同时支持ZIP压缩包和直接生成到项目两种模式，解放你的双手，大大减少了重复代码的编写,无需复制提升开发效率。
<img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/代码生成.png"/>

#### 📖详细的注释和使用文档
作为一个开源项目，如何让使用者看懂代码是非常重要的，很多作者会忽视这一点，导致我们阅读其代码的时候很难理解其为什么要这么写,如果没有详细的文档，想要二次开发需要花精力去研究源码。而本系统完全不用担心这个问题，后端源码注释覆盖率超过<font color="#dd0000">90%</font><br /> 
后续每个功能模块也会推出相应的说明文档，目的就是让使用者能够轻易上手，就算不是自己的代码，也能轻易看懂。
<img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/wendang.png"/>

## 📺效果图📺
<table>
    <tr>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/1.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/2.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/3.png"/></td>
    </tr>
      <tr>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/4.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/5.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/6.png"/></td>
    </tr>
      <tr>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/7.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/8.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/9.png"/></td>
    </tr>
      <tr>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/10.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/11.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/12.png"/></td>
    </tr>
      <tr>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/13.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/14.png"/></td>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/15.png"/></td>
    </tr>
    <tr>
       <td><img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/16.png"/></td>
    </tr>

</table>

## 🚑快速启动🚑
#### 🚀启动前端

如果没有安装 Node.js 16，下载地址：[https://nodejs.org](https://nodejs.org)

```
npm install
```
```
npm run dev
```
#### 🛩️启动后端
编辑`Core.Development.json`文件,配置Redis地址和数据库地址,设置`SimpleAdmin.Web.Entry`为启动项目，直接启动项目即可。

<img src="https://gitee.com/zxzyjs/SimpleAdmin/raw/master/doc/Image/后端启动.png"/>

## 🎞️演示地址
1. 地址1：[http://admin.zxzyjs.com](http://admin.zxzyjs.com)
2. 地址2：[http://121.43.166.188:12345](http://121.43.166.188:12345)
3. 账号：superAdmin
4. 密码：123456

## 🎈相关连接🎈

更新日志：[点击查看](https://gitee.com/zxzyjs/SimpleAdmin/commits/master)

文档地址：[https://www.cnblogs.com/huguodong/p/17021233.html](https://www.cnblogs.com/huguodong/p/17021233.html)

常见问题合集：[https://www.cnblogs.com/huguodong/p/17021241.html](https://www.cnblogs.com/huguodong/p/17021241.html)

## 🔖友情链接🔖
- 👉 Furion：[https://dotnetchina.gitee.io/furion](https://dotnetchina.gitee.io/furion)
-  👉 SqlSugar：[https://www.donet5.com/Doc/1/1180](https://www.donet5.com/Doc/1/1180)
-  👉 NewLife：[https://www.newlifex.com/](https://www.newlifex.com/)
-  👉 Snowy：[https://xiaonuo.vip/doc](https://xiaonuo.vip/doc)
-  👉 IdGenerator：[https://github.com/yitter/idgenerator](https://github.com/yitter/idgenerator)
-  👉 Masuit.Tools：[https://gitee.com/masuit/Masuit.Tools](https://gitee.com/masuit/Masuit.Tools)
-  👉 Emqx：[https://www.emqx.com/zh](https://www.emqx.com/zh)
  
## 💾版权声明💾

- 后端源码完全免费开源商用。
- 前端源码尊重小诺开源协议，不能作为开源竞品，但你任然可以用于个人项目等接私活或企业项目脚手架使用，为防止侵权，如需获取前端源码授权请[加我](https://qm.qq.com/cgi-bin/qm/qr?k=of373DKaD1xCdNfz8DskOwauCguHfMrS&noverify=0&personal_qrcode_source=4)获取授权，或者直接咸鱼搜simpleadmin
- 不能以任何形式用于非法为目的的行为。
- 任何基于本软件而产生的一切法律纠纷和责任，均于作者无关。
