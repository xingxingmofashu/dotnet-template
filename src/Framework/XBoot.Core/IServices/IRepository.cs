using SqlSugar;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using XBoot.Composables.Enums;
using XBoot.Core.Model;

namespace XBoot.Core.IServices
{
    public interface IRepository<TEntity> where TEntity : class, IEntityBase, new()
    {
        SqlSugarScope GetSqlSugarClient();

        /// <summary>
        /// 创建Guid
        /// </summary>
        /// <returns></returns>
        Guid CreateNewGuid();

        #region 原生Sql

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>返回影响行数</returns>
        Task<int> ExecuteCommandAsync(string sql);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>DataTable</returns>
        Task<DataTable> GetDataTableAsync(string sql);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>object</returns>
        Task<object> GetScalarAsync(string sql);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>object</returns>
        Task<int> GetIntAsync(string sql);

        #endregion 原生Sql

        #region 事务操作

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 事务提交
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 事务回滚
        /// </summary>
        void RollbackTransaction();

        #endregion 事务操作

        #region Query

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns>是,否</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        /// 根据TEntity 条件 获取TEntity 集合
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns>ISugarQueryable<TEntity></returns>
        ISugarQueryable<TEntity> Query(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        /// 获取数据库数据
        /// </summary>
        /// <returns></returns>
        ISugarQueryable<TEntity> Query();

        /// <summary>
        /// 获取动态实体数据
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp);

        /// <summary>
        /// 通过主键查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TEntity</returns>
        Task<TEntity> GetAsync(long id);

        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="exp">表达式</param>
        /// <returns>是，否</returns>
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> exp) where T : EntityBase, new();

        /// <summary>
        /// 非异步-是否存在记录
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="exp">表达式</param>
        /// <returns>是，否</returns>
        bool Any<T>(Expression<Func<T, bool>> exp) where T : EntityBase, new();

        /// <summary>
        /// 根据筛选条件查询
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> exp) where T : EntityBase, new();

        /// <summary>
        /// 根据筛选条件查询
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        ISugarQueryable<T> Query<T>() where T : EntityBase, new();

        /// <summary>
        /// 根据条件 获取用户信息
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="exp"></param>
        /// <returns>单个实体对象</returns>
        Task<T> GetAsync<T>(Expression<Func<T, bool>> exp) where T : EntityBase, new();

        /// <summary>
        /// 根据主键获取用户信息
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(long id) where T : EntityBase, new();

        /// <summary>
        /// 同步方法：根据主键获取用户信息
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get<T>(long id) where T : EntityBase, new();

        /// <summary>
        /// 根据固定条件表达式，获取数据集合
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="desc">是否降序 默认降序</param>
        /// <returns>泛型集合</returns>
        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderBy = null, bool desc = true) where T : EntityBase, new();

        /// <summary>
        /// 根据固定条件表达式，获取数据集合
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="desc">是否降序 默认降序</param>
        /// <returns>泛型集合</returns>
        List<T> GetList<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderBy = null, bool desc = true) where T : EntityBase, new();

        /// <summary>
        /// 根据固定条件表达式，获取数据集合【同步】
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="desc">是否降序 默认降序</param>
        /// <returns>泛型集合</returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>? orderBy = null, bool desc = true);

        #endregion Query

        #region Add

        /// <summary>
        /// 创建单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>返回影响函数</returns>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// 创建批量实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns>返回影响行数</returns>
        Task<int> InsertAsync(List<TEntity> entitys);

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="entity"></param>
        /// <returns>返回影响行数</returns>
        Task<int> AddAsync<T>(T entity) where T : EntityBase, new();

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="entitys"></param>
        /// <returns>返回影响行数</returns>
        Task<int> AddsAsync<T>(List<T> entitys) where T : EntityBase, new();

        /// <summary>
        /// 插入单个对象
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="model">对象模型</param>
        /// <returns>返回雪花主键</returns>
        Task<long> InsertBySnowflake<T>(T model) where T : EntityBase, new();

        /// <summary>
        /// 通过字典创建数据
        /// 注意：非自增主键 不能用
        /// </summary>
        /// <typeparam name="columnDictionary">字典类型</typeparam>
        /// <returns>返回自增主键</returns>
        Task<long> InsertReturnIdentity<T>(Dictionary<string, object> columnDictionary) where T : new();

        /// <summary>
        /// 批量插入对象
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="models">对象模型集合</param>
        /// <returns>返回雪花主键集合</returns>
        Task<List<long>> InsertBySnowflakes<T>(List<T> models) where T : EntityBase, new();

        /// <summary>
        /// 单个插入
        /// </summary>
        /// <typeparam name="T">动态实体</typeparam>
        /// <param name="mod">对象模型</param>
        /// <param name="ignoreColumn">忽略属性列</param>
        /// <returns>返回创建成功后的数据实体</returns>
        Task<T> InsertReturnTAsync<T>(T mod, params string[] ignoreColumn) where T : class, new();

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T">动态实体</typeparam>
        /// <param name="mods">对象模型</param>
        /// <param name="ignoreColumn">忽略列</param>
        /// <returns>返回影响行数</returns>
        Task<int> InsertListAsync<T>(List<T> mods, params string[] ignoreColumn) where T : class, new();

        #endregion Add

        #region Update

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="wherexp">条件表达式</param>
        /// <param name="upexp">更新实体列</param>
        /// <returns>返回影响函行数</returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> wherexp, Expression<Func<TEntity, bool>> upexp);

        /// <summary>
        /// DB模型更新数据
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="wherexp">筛选条件</param>
        /// <param name="upexp">忽略部分列</param>
        /// <returns>返回影响函数</returns>
        Task<int> UpdateAsync<T>(Expression<Func<T, bool>> wherexp, Expression<Func<T, T>> upexp) where T : EntityBase, new();

        /// <summary>
        /// 更新单个对象
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="model">对象模型</param>
        /// <param name="columns">指定需要更新的字段表达式，如果为空，则更新所有的字段</param>
        /// <returns>返回受影响的行数</returns>
        Task<int> UpdateAsync<T>(T model, Expression<Func<T, object>>? columns = null) where T : EntityBase, new();

        /// <summary>
        /// 更新单个对象
        /// </summary>
        /// <typeparam name="T">动态实体</typeparam>
        /// <param name="columnDictionary">字典类型</param>
        /// <returns>返回影响行数</returns>
        Task<int> UpdateAsync<T>(Dictionary<string, object> columnDictionary) where T : new();

        /// <summary>
        /// 批量更新对象
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="models">对象模型集合</param>
        /// <param name="columns">指定需要更新的字段表达式，如果为空，则更新所有的字段</param>
        /// <returns>返回受影响的行数</returns>
        Task<int> UpdateAsync<T>(List<T> models, Expression<Func<T, object>>? columns = null) where T : EntityBase, new();

        #endregion Update

        #region Delete

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="wherexp">条件表达式</param>
        /// <returns>返回影响行数</returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> wherexp);

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <typeparam name="T">DB数据模型</typeparam>
        /// <param name="wherexp">筛选条件</param>
        /// <returns>返回影响行数</returns>
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> wherexp) where T : EntityBase, new();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="models">对象模型</param>
        /// <returns>返回受影响的行数</returns>
        Task<int> Delete<T>(List<T> models) where T : EntityBase, new();

        /// <summary>
        /// 根据主键ID删除
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns>返回受影响的行数</returns>
        Task<int> Delete<T>(Guid id) where T : EntityBase, new();

        /// <summary>
        /// 根据主键ID逻辑删除
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="ids">主键Id集合</param>
        /// <returns>返回受影响的行数</returns>
        Task<int> DeleteByIds<T>(List<Guid> ids) where T : EntityBase, new();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">动态表对象</typeparam>
        /// <param name="model"></param>
        /// <returns>返回影响函数</returns>
        Task<int> Delete<T>(T model) where T : class, new();

        /// <summary>
        /// 根据条件删除 物理删除,数据彻底从数据库移除
        /// </summary>
        /// <typeparam name="T">公共DB模型</typeparam>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<int> DeleteDeep<T>(Expression<Func<T, bool>> predicate) where T : class, new();

        #endregion Delete

        #region 事务扩展

        /// <summary>
        /// 事务操作 Action返回的对象中，必须使用原生SqlSugarScope进行数据库操作
        /// </summary>
        /// <param name="actions">操作函数集合</param>
        /// <returns>返回影响行数</returns>
        int DbTransaction(params Func<SqlSugarScope, int>[] actions);

        /// <summary>
        /// 事物批量执行,根据不同的对象，指定对象的数据操作方式
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        int DbTransaction(Dictionary<dynamic, SqlAction> models);

        #endregion 事务扩展

        #region sql脚本

        /// <summary>
        /// 参数化执行sql脚本
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <param name="sqlparms">参数数组</param>
        /// <returns>返回影响行数</returns>
        Task<int> ExcuteSqlAsync(string sql, DbParameter[]? sqlparms = null);

        /// <summary>
        /// sql脚本 获取单个对象
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <param name="sqlparms">参数数组</param>
        /// <returns>返回影响行数</returns>
        Task<T> QuerySqlSingleAsync<T>(string sql, object? sqlparms = null);

        /// <summary>
        /// sql脚本 获取查询列表数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlparms"></param>
        /// <returns>返回影响行数</returns>
        Task<List<T>> QuerySqlListAsync<T>(string sql, object? sqlparms = null);

        /// <summary>
        /// SqlQueryable
        /// </summary>
        /// <typeparam name="T">动态实体对象</typeparam>
        /// <param name="sqlStr">sql脚本</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回IQueryable对象</returns>
        IQueryable<T> SqlQueryable<T>(string sqlStr, params object[] parameters);

        /// <summary>
        /// SqlQueryable
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sqlStr">sql脚本</param>
        /// <returns></returns>
        ISugarQueryable<T> SqlQueryable<T>(string sqlStr) where T : class, new();

        /// <summary>
        /// SQL查询
        /// </summary>
        /// <typeparam name="T">动态实体</typeparam>
        /// <param name="predicate">表达式-筛选条件</param>
        /// <param name="sqlStr">sql脚本</param>
        /// <returns>返回List<T></returns>
        List<T> SqlQuery<T>(Expression<Func<T, bool>> predicate, string sqlStr);

        /// <summary>
        /// SQL查询
        /// </summary>
        /// <typeparam name="T">动态实体</typeparam>
        /// <param name="predicates">表达式-筛选条件</param>
        /// <param name="sqlStr">sql脚本</param>
        /// <returns>返回List<T></returns>
        List<T> SqlQuery<T>(List<Expression<Func<T, bool>>> predicates, string sqlStr);

        #endregion sql脚本

        #region 分页查询-表达式

        /// <summary>
        /// 泛型分页查询 返回tuple类型
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="whereExpression">筛选条件</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="orderByFileds">排序条件</param>
        /// <returns></returns>
        Task<(List<T> T, RefAsync<int> totalCount, int pageCount, int pageIndex, int pageSize)> QueryPageAsync<T>(Expression<Func<T, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, string? orderByFileds = null) where T : EntityBase, new();

        /// <summary>
        /// 根据固定条件表达式，获取分组分页数据集合
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="predicate">条件表达式</param>
        /// <param name="total">总数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="desc">是否降序 默认降序</param>
        /// <returns>返回dynamic集合</returns>
        Task<dynamic> GetGroupByPageListAsync<T>(Expression<Func<T, bool>> predicate, RefAsync<int> total, int pageIndex = 1, int pageSize = 10, Expression<Func<T, object>>? groupBys = null, Expression<Func<T, object>>? selects = null, Expression<Func<T, object>>? orderBy = null, bool desc = true) where T : EntityBase, new();

        /// <summary>
        /// 表达式分页查询
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="total">总数</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="desc">是否降序 默认降序</param>
        /// <returns>返回List<T></returns>
        Task<List<T>> PageListAsync<T>(int pageIndex, int pageSize, RefAsync<int> total, Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderBy = null, bool desc = true) where T : EntityBase, new();

        #endregion 分页查询 之 表达式

        #region 分页查询-sql脚本

        /// <summary>
        /// 分页读取列表
        /// </summary>
        /// <typeparam name="T">分页信息类</typeparam>
        /// <param name="count">记录条数</param>
        /// <param name="table">表名 例： table1 inner join table2 on table1.xx=table2.xx</param>
        /// <param name="colList">需要获取字段 例: tabl1.xx,table2.*,注意，需要把排序列都选上</param>
        /// <param name="whereStr">条件,不带where</param>
        /// <param name="orderBy">最内层orderby  例:  order by table1.xxx desc,table2.ad asc "</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="maxCount">最大获取数,当设置此值时Count返回不会超过这个限定值，设置该值可以提升查询效率，建意这个值在10W以内</param>
        /// <returns>列表</returns>
        IList<T>? GetPageList<T>(out int count, string table, string colList, string whereStr, string orderBy, int pageIndex, int pageSize = 10, int maxCount = 0) where T : new();

        /// <summary>
        /// 分页读取列表
        /// </summary>
        /// <typeparam name="T">分页信息类</typeparam>
        /// <param name="table">表名 例： table1 inner join table2 on table1.xx=table2.xx</param>
        /// <param name="colList">需要获取字段 例: tabl1.xx,table2.*,注意，需要把排序列都选上</param>
        /// <param name="where">条件,不带where</param>
        /// <param name="orderBy">最内层orderby  例: order by table1.xxx desc,table2.ad asc "</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="MaxCount">最大获取数,当设置此值时Count返回不会超过这个限定值，设置该值可以提升查询效率，建意这个值在10W以内</param>
        /// <returns>元组：IList,总数</returns>
        Task<(IList<T>? list, RefAsync<int> totalCount)> PageListAsync<T>(string table, string colList, string where, string orderBy, int pageIndex, int pageSize = 10, int MaxCount = 0) where T : new();

        /// <summary>
        /// 分页读取列表
        /// </summary>
        /// <param name="table">表名 例： table1 inner join table2 on table1.xx=table2.xx</param>
        /// <param name="colList">需要获取字段 例: tabl1.xx,table2.*,注意，需要把排序列都选上</param>
        /// <param name="whereStr">条件,不带where</param>
        /// <param name="orderBy">最内层orderby  例:  order by table1.xxx desc,table2.ad asc "</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="maxCount">最大获取数,当设置此值时Count返回不会超过这个限定值，设置该值可以提升查询效率，建意这个值在10W以内</param>
        /// <returns>元组：DataTable,总数</returns>
        Task<(DataTable result, RefAsync<int> count)> PageListTable(string table, string colList, string whereStr, string orderBy, int pageIndex, int pageSize = 10, int maxCount = 0);

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <param name="parameters"></param>
        /// <returns>返回DataTable</returns>
        Task<DataTable> GetDataTableAsync(string sql, params SugarParameter[] parameters);

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <param name="parameters"></param>
        /// <returns>返回DataTable</returns>
        Task<DataTable> GetDataTableAsync(string sql, List<SugarParameter> parameters);

        #endregion 分页查询 之 sql脚本

        #region 存储过程

        /// <summary>
        /// 查询存储过程
        /// </summary> 
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataSet</returns>
        Task<DataSet> QueryProcedureDataSet(string procedureName, List<SugarParameter> parameters);

        /// <summary>
        /// 查询存储过程
        /// </summary> 
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        Task<DataTable> QueryProcedureDataTable(string procedureName, List<SugarParameter> parameters);

        /// <summary>
        /// 查询存储过程
        /// </summary> 
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>单个值</returns>
        Task<object> QueryProcedureScalar(string procedureName, List<SugarParameter> parameters);

        #endregion

        #region SqlBulkCopy

        /// <summary>
        /// 并发高用异步方法吐能量能提高,
        /// 一次插入的数据量越大他的价值越高，比如1000以下就不合算了
        /// ，数据量小的时候并发吞吐量不如普通插入
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="lstData"></param>
        /// <returns>返回影响的行数</returns>
        Task<int> InsertBulkCopyAsync<T>(List<T> lstData) where T : EntityBase, new();

        /// <summary>
        /// 并发高用异步方法吐能量能提高,
        /// 一次插入的数据量越大他的价值越高，比如1000以下就不合算了
        /// ，数据量小的时候并发吞吐量不如普通插入
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="lstData"></param>
        /// <returns>返回影响的行数</returns>
        Task<int> UpdateBulkCopyAsync<T>(List<T> lstData) where T : EntityBase, new();

        /// <summary>
        /// 同时插入及更新
        /// </summary>
        /// <typeparam name="T">DB模型</typeparam>
        /// <param name="lstData"></param>
        /// <param name="columns">非Id的自定义主键</param>
        /// <param name="ignoreColumns">忽略字段集合</param>
        /// <returns>返回影响的行数Tuple</returns>
        Task<(int insert, int update)> AddOrUpdateAsync<T>(List<T> lstData, Expression<Func<T, object>>? columns = null, string[]? ignoreColumns = null) where T : EntityBase, new();

        Task<int> InsertListCreatedIdAsync<T>(List<T> lstData) where T : EntityBase, new();
        #endregion SqlBulkCopy

    }
}