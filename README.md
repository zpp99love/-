### 01项目介绍
这是一个简单的领域->服务->控制器模板，通过接口完成仓储实现类和服务实现类可拓展，并搭配依赖注入实现调用

### 02项目介绍
测试多个ActionFilter的执行顺序和不同地方发生异常的处理规律

### 03项目介绍
测试ActionFilter结合内存缓存拦截Action请求进行限流

### 04项目介绍
测试ActionFilter结合TransactionScope进行Action对数据库操作的事务回滚、涉及到了Attribute标记的实现

### 05项目介绍
测试中间件的基本使用，并实现简单的自定义中间件处理请求

### 06项目介绍
测试中间件将后端md文本加工为html格式传给浏览器，并体现了中间件和过滤器的工作层次不同，展示对于非控制器非Action的请求应该由中间件处理的思考

### 07项目介绍
测试ILogger生成日志、NLog和Serilog加工日志、日志匹配规则、日志输出文本、结构化日志、集中化日志分析

### 08项目介绍
测试客户端([ResponseCache(Duration=60)])缓存、服务端(IMemoryCache)缓存、缓存穿透、缓存雪崩、绝对滑动过期时间策略；以及测试多ExceptionFilter的处理截断情况

### 09项目介绍
测试EFCore三种表关系的建立的不同场景和方案、导航属性的单双向性、FluentAPI配置、迁移命令(双数据库)、加深对DbContext的构造函数、连接字符串配置、表配置、和依赖注入理解

### 10项目介绍
测试对于点赞这样的高并发操作导致数据覆盖问题，使用悲观(事务锁)和EFCore的乐观(单字段和RowVersion)方案如何处理、选择
