using SqlSugar;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using XBoot.Composables.Enums;
using XBoot.Core.IServices;
using XBoot.Core.Model;

namespace XBoot.Core.Services;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase, new()
{
    private readonly SqlSugarScope _client;

    public Repository(SqlSugarScope client)
    {
        _client = client;
    }

    /// <summary>
    /// 返回sqlClient对象
    /// </summary>
    /// <returns></returns>
    public SqlSugarScope GetSqlSugarClient()
    {
        return _client;
    }

    /// <summary>
    /// 创建Guid
    /// </summary>
    /// <returns></returns>
    public Guid CreateNewGuid()
    {
        return Guid.NewGuid();
    }

    #region 原生Sql

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> ExecuteCommandAsync(string sql)
    {
        return await _client.Ado.ExecuteCommandAsync(sql);
    }

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>DataTable</returns>
    public virtual async Task<DataTable> GetDataTableAsync(string sql)
    {
        return await _client.Ado.GetDataTableAsync(sql);
    }

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>object</returns>
    public virtual async Task<object> GetScalarAsync(string sql)
    {
        return await _client.Ado.GetScalarAsync(sql);
    }

    public virtual async Task<int> GetIntAsync(string sql)
    {
        return await _client.Ado.GetIntAsync(sql);
    }

    #endregion 原生Sql

    #region 事务操作an

    /// <summary>
    /// 开始事务
    /// </summary>
    public void BeginTransaction() => _client.BeginTran();

    /// <summary>
    /// 事务提交
    /// </summary>
    public void CommitTransaction() => _client.CommitTran();

    /// <summary>
    /// 事务回滚
    /// </summary>
    public void RollbackTransaction() => _client.RollbackTran();

    #endregion 事务操作an

    #region Query

    /// <summary>
    /// 判断对象是否存在
    /// </summary>
    /// <param name="exp">条件表达式</param>
    /// <returns>是,否</returns>
    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> exp)
    {
        return await _client.Queryable<TEntity>().With(SqlWith.NoLock).AnyAsync(exp);
    }

    /// <summary>
    /// 根据TEntity 条件 获取TEntity 集合
    /// </summary>
    /// <param name="exp">条件表达式</param>
    /// <returns>ISugarQueryable<TEntity></returns>
    public virtual ISugarQueryable<TEntity> Query(Expression<Func<TEntity, bool>> exp)
    {
        if (null == exp)
        {
            return _client.Queryable<TEntity>().With(SqlWith.NoLock);
        }
        else
        {
            return _client.Queryable<TEntity>().With(SqlWith.NoLock).Where(exp);
        }
    }

    /// <summary>
    /// 根据TEntity 条件 获取TEntity 集合
    /// </summary>
    /// <param name="exp">条件表达式</param>
    /// <returns>ISugarQueryable<TEntity></returns>
    public virtual ISugarQueryable<TEntity> Query()
    {
        return _client.Queryable<TEntity>().With(SqlWith.NoLock);
    }

    /// <summary>
    /// 获取动态实体数据
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
    {
        return await _client.Queryable<TEntity>().With(SqlWith.NoLock).FirstAsync(exp);
    }

    /// <summary>
    /// 通过主键查询实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns>TEntity</returns>
    public virtual async Task<TEntity> GetAsync(long id)
    {
        return await _client.Queryable<TEntity>().With(SqlWith.NoLock).InSingleAsync(id);
    }

    /// <summary>
    /// 是否存在记录
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="exp">表达式</param>
    /// <returns>是，否</returns>
    public virtual async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> exp) where T : EntityBase, new()
    {
        return await _client.Queryable<T>().With(SqlWith.NoLock).AnyAsync(exp);
    }

    /// <summary>
    /// 非异步-是否存在记录
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="exp">表达式</param>
    /// <returns>是，否</returns>
    public virtual bool Any<T>(Expression<Func<T, bool>> exp) where T : EntityBase, new()
    {
        return _client.Queryable<T>().With(SqlWith.NoLock).Any(exp);
    }

    /// <summary>
    /// 根据筛选条件查询
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="exp"></param>
    /// <returns></returns>
    public virtual ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> exp) where T : EntityBase, new()
    {
        return _client.Queryable<T>().With(SqlWith.NoLock).Where(exp);
    }

    public virtual ISugarQueryable<T> Query<T>() where T : EntityBase, new()
    {
        return _client.Queryable<T>().With(SqlWith.NoLock);
    }

    /// <summary>
    /// 根据条件 获取用户信息
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="exp"></param>
    /// <returns>单个实体对象</returns>
    public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> exp) where T : EntityBase, new()
    {
        return await _client.Queryable<T>().With(SqlWith.NoLock).Where(exp).FirstAsync();
    }

    /// <summary>
    /// 根据主键获取用户信息
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<T> GetAsync<T>(long id) where T : EntityBase, new()
    {
        return await _client.Queryable<T>().With(SqlWith.NoLock).InSingleAsync(id);
    }

    /// <summary>
    /// 根据主键获取用户信息
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual T Get<T>(long id) where T : EntityBase, new()
    {
        return _client.Queryable<T>().With(SqlWith.NoLock).InSingle(id);
    }

    /// <summary>
    /// 根据固定条件表达式，获取数据集合
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="predicate">条件表达式</param>
    /// <param name="orderBy">排序表达式</param>
    /// <param name="desc">是否降序 默认降序</param>
    /// <returns>泛型集合</returns>
    public virtual async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderBy = null, bool desc = true) where T : EntityBase, new()
    {
        ISugarQueryable<T>? query = _client.Queryable<T>().With(SqlWith.NoLock).Where(predicate);
        if (orderBy != null)
        {
            query = desc ? query.OrderBy(orderBy, OrderByType.Desc) : query.OrderBy(orderBy, OrderByType.Asc);
        }
        return await query.ToListAsync();
    }

    /// <summary>
    /// 根据固定条件表达式，获取数据集合
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>? orderBy = null, bool desc = true)
    {
        ISugarQueryable<TEntity>? query = _client.Queryable<TEntity>().With(SqlWith.NoLock).Where(predicate);
        if (orderBy != null)
        {
            query = desc ? query.OrderBy(orderBy, OrderByType.Desc) : query.OrderBy(orderBy, OrderByType.Asc);
        }
        return await query.ToListAsync();
    }

    /// <summary>
    /// 根据固定条件表达式，获取数据集合【同步】
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="predicate">条件表达式</param>
    /// <param name="orderBy">排序表达式</param>
    /// <param name="desc">是否降序 默认降序</param>
    /// <returns>泛型集合</returns>
    public virtual List<T> GetList<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderBy = null, bool desc = true) where T : EntityBase, new()
    {
        ISugarQueryable<T>? query = _client.Queryable<T>().With(SqlWith.NoLock).Where(predicate);
        if (orderBy != null)
        {
            query = desc ? query.OrderBy(orderBy, OrderByType.Desc) : query.OrderBy(orderBy, OrderByType.Asc);
        }
        return query.ToList();
    }

    #endregion Query

    #region Add

    /// <summary>
    /// 创建单个实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>返回影响函数</returns>
    public virtual async Task<int> InsertAsync(TEntity entity)
    {
        return await _client.Insertable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 创建批量实体
    /// </summary>
    /// <param name="entitys"></param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> InsertAsync(List<TEntity> entitys)
    {
        return await _client.Insertable(entitys).ExecuteCommandAsync();
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="entity"></param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> AddAsync<T>(T entity) where T : EntityBase, new()
    {
        if (string.IsNullOrEmpty(entity.Id.ToString()))
        {
            entity.Id = Guid.NewGuid();
        }
        return await _client.Insertable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="entitys"></param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> AddsAsync<T>(List<T> entitys) where T : EntityBase, new()
    {
        return await _client.Insertable(entitys).ExecuteCommandAsync();
    }

    /// <summary>
    /// 插入单个对象
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="model">对象模型</param>
    /// <returns>返回雪花主键</returns>
    public virtual async Task<long> InsertBySnowflake<T>(T model) where T : EntityBase, new()
    {
        return await _client.Insertable(model).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 通过字典创建数据
    /// 注意：非自增主键 不能用
    /// </summary>
    /// <typeparam name="columnDictionary">字典类型</typeparam>
    /// <returns>返回自增主键</returns>
    public virtual async Task<long> InsertReturnIdentity<T>(Dictionary<string, object> columnDictionary) where T : new()
    {
        return await _client.Insertable(columnDictionary).ExecuteReturnBigIdentityAsync();
    }

    /// <summary>
    /// 批量插入对象
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="models">对象模型集合</param>
    /// <returns>返回雪花主键集合</returns>
    public virtual async Task<List<long>> InsertBySnowflakes<T>(List<T> models) where T : EntityBase, new()
    {
        return await _client.Insertable(models).ExecuteReturnSnowflakeIdListAsync();
    }

    /// <summary>
    /// 单个插入
    /// </summary>
    /// <typeparam name="T">动态实体</typeparam>
    /// <param name="mod">对象模型</param>
    /// <param name="ignoreColumn">忽略属性列</param>
    /// <returns>返回创建成功后的数据实体</returns>
    public virtual async Task<T> InsertReturnTAsync<T>(T mod, params string[] ignoreColumn) where T : class, new()
    {
        return await _client.Insertable(mod).IgnoreColumns(ignoreColumn).ExecuteReturnEntityAsync();
    }

    /// <summary>
    /// 批量插入
    /// </summary>
    /// <typeparam name="T">动态实体</typeparam>
    /// <param name="mods">对象模型</param>
    /// <param name="ignoreColumn">忽略列</param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> InsertListAsync<T>(List<T> mods, params string[] ignoreColumn) where T : class, new()
    {
        return await _client.Insertable(mods.ToArray()).IgnoreColumns(ignoreColumn).ExecuteCommandAsync();
    }

    #endregion Add

    #region Update

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="wherexp">条件表达式</param>
    /// <param name="upexp">更新实体列</param>
    /// <returns>返回影响函行数</returns>
    public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> wherexp, Expression<Func<TEntity, bool>> upexp)
    {
        return await _client.Updateable<TEntity>().Where(wherexp).SetColumns(upexp).ExecuteCommandAsync();
    }

    /// <summary>
    /// DB模型更新数据
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="wherexp">筛选条件</param>
    /// <param name="upexp">忽略部分列</param>
    /// <returns>返回影响函数</returns>
    public virtual async Task<int> UpdateAsync<T>(Expression<Func<T, bool>> wherexp, Expression<Func<T, T>> upexp) where T : EntityBase, new()
    {
        return await _client.Updateable<T>().Where(wherexp).SetColumns(upexp).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新单个对象
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="model">对象模型</param>
    /// <param name="columns">指定需要更新的字段表达式，如果为空，则更新所有的字段</param>
    /// <returns>返回受影响的行数</returns>
    public virtual async Task<int> UpdateAsync<T>(T model, Expression<Func<T, object>>? columns = null) where T : EntityBase, new()
    {
        if (columns != null)
        {
            return await _client.Updateable(model).UpdateColumns(columns).IgnoreColumns(m => new { m.CreatedBy, m.CreatedTime }).ExecuteCommandAsync();
        }
        return await _client.Updateable(model).IgnoreColumns(m => new { m.CreatedBy, m.CreatedTime }).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新单个对象
    /// </summary>
    /// <typeparam name="T">动态实体</typeparam>
    /// <param name="columnDictionary">字典类型</param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> UpdateAsync<T>(Dictionary<string, object> columnDictionary) where T : new()
    {
        return await _client.Updateable(columnDictionary).ExecuteCommandAsync();
    }

    /// <summary>
    /// 批量更新对象
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="models">对象模型集合</param>
    /// <param name="columns">指定需要更新的字段表达式，如果为空，则更新所有的字段</param>
    /// <returns>返回受影响的行数</returns>
    public virtual async Task<int> UpdateAsync<T>(List<T> models, Expression<Func<T, object>>? columns = null) where T : EntityBase, new()
    {
        if (columns != null)
            return await _client.Updateable(models).UpdateColumns(columns).IgnoreColumns(m => new { m.CreatedBy, m.CreatedTime }).ExecuteCommandAsync();
        return await _client.Updateable(models).IgnoreColumns(m => new { m.CreatedBy, m.CreatedTime }).ExecuteCommandAsync();
    }

    #endregion Update

    #region Delete

    /// <summary>
    /// 物理删除/逻辑删除
    /// </summary>
    /// <param name="wherexp">条件表达式</param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> wherexp)
    {
        if (typeof(TEntity).IsAssignableTo(typeof(IEntityBase)))
        {
            return await _client.Deleteable<TEntity>().Where(wherexp).IsLogic().ExecuteCommandAsync();
        }
        else
        {
            return await _client.Deleteable<TEntity>().Where(wherexp).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 物理删除/逻辑删除
    /// </summary>
    /// <typeparam name="T">DB数据模型</typeparam>
    /// <param name="wherexp">筛选条件</param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> wherexp) where T : EntityBase, new()
    {
        if (typeof(T).IsAssignableTo(typeof(IEntityBase)))
        {
            return await _client.Deleteable<T>().Where(wherexp).IsLogic().ExecuteCommandAsync();
        }
        else
        {
            return await _client.Deleteable<T>().Where(wherexp).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 物理删除/逻辑删除
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="models">对象模型</param>
    /// <returns>返回受影响的行数</returns>
    public virtual async Task<int> Delete<T>(List<T> models) where T : EntityBase, new()
    {
        if (models == null)
            return 0;
        if (typeof(T).IsAssignableTo(typeof(IEntityBase)))
        {
            return await _client.Deleteable(models).IsLogic().ExecuteCommandAsync();
        }
        else
        {
            return await _client.Deleteable(models).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 根据主键ID 物理删除/逻辑删除
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="id">主键ID</param>
    /// <returns>返回受影响的行数</returns>
    public virtual async Task<int> Delete<T>(Guid id) where T : EntityBase, new()
    {
        if (typeof(T).IsAssignableTo(typeof(IEntityBase)))
        {
            return await _client.Deleteable<T>().Where(x => x.Id == id).IsLogic().ExecuteCommandAsync();
        }
        else
        {
            return await _client.Deleteable<T>().Where(x => x.Id == id).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 根据主键ID 物理删除/逻辑删除
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="ids">主键Id集合</param>
    /// <returns>返回受影响的行数</returns>
    public virtual async Task<int> DeleteByIds<T>(List<Guid> ids) where T : EntityBase, new()
    {
        if (ids == null) return 0;
        if (typeof(T).IsAssignableTo(typeof(IEntityBase)))
        {
            return await _client.Deleteable<T>().Where(x => ids.Contains(x.Id)).IsLogic().ExecuteCommandAsync();
        }
        else
        {
            return await _client.Deleteable<T>().Where(x => ids.Contains(x.Id)).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <typeparam name="T">动态表对象</typeparam>
    /// <param name="model"></param>
    /// <returns>返回影响函数</returns>
    public virtual async Task<int> Delete<T>(T model) where T : class, new()
    {
        if (model == null)
            return 0;
        if (typeof(T).IsAssignableTo(typeof(IEntityBase)))
        {
            return await _client.Deleteable(model).IsLogic().ExecuteCommandAsync();
        }
        else
        {
            return await _client.Deleteable(model).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 根据条件删除 物理删除,数据彻底从数据库移除
    /// </summary>
    /// <typeparam name="T">公共DB模型</typeparam>
    /// <param name="predicate">条件表达式</param>
    /// <returns></returns>
    public virtual async Task<int> DeleteDeep<T>(Expression<Func<T, bool>> predicate) where T : class, new()
    {
        if (typeof(T).IsAssignableTo(typeof(IEntityBase)))
        {
            return await _client.Deleteable(predicate).IsLogic().ExecuteCommandAsync();
        }
        else
        {
            return await _client.Deleteable(predicate).ExecuteCommandAsync();
        }
    }

    #endregion Delete

    #region 事务扩展

    /// <summary>
    /// 事务操作 Action返回的对象中，必须使用原生SqlSugarClient进行数据库操作
    /// </summary>
    /// <param name="actions">操作函数集合</param>
    /// <returns>返回影响行数</returns>
    public int DbTransaction(params Func<SqlSugarScope, int>[] actions)
    {
        int iRet = 0;
        DbResult<bool>? bRet = _client.Ado.UseTran(() =>
        {
            foreach (Func<SqlSugarScope, int>? action in actions)
            {
                int ret = action(_client);
                iRet += ret;
            }
        });
        if (bRet.IsSuccess)
        {
            return iRet;
        }
        throw new Exception(bRet.ErrorMessage);
    }

    /// <summary>
    /// 事物批量执行,根据不同的对象，指定对象的数据操作方式
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int DbTransaction(Dictionary<dynamic, SqlAction> models)
    {
        int iRet = 0;
        DbResult<bool>? bRet = _client.Ado.UseTran(() =>
        {
            foreach (KeyValuePair<dynamic, SqlAction> item in models)
            {
                iRet += item.Value switch
                {
                    SqlAction.Add => _client.Insertable(item.Key).ExecuteCommand(),
                    SqlAction.Delete => _client.Deleteable(item.Key).ExecuteCommand(),
                    SqlAction.Modify => _client.Updateable(item.Key).ExecuteCommand(),
                    _ => 0,
                };
            }
        });
        if (bRet.IsSuccess)
        {
            return iRet;
        }
        throw new Exception(bRet.ErrorMessage);
    }

    #endregion 事务扩展

    #region sql脚本

    /// <summary>
    /// 参数化执行sql脚本
    /// </summary>
    /// <param name="sql">sql脚本</param>
    /// <param name="sqlparms">参数数组</param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<int> ExcuteSqlAsync(string sql, DbParameter[]? sqlparms = null)
    {
        return await _client.Ado.ExecuteCommandAsync(sql, sqlparms);
    }

    /// <summary>
    /// sql脚本 获取单个对象
    /// </summary>
    /// <param name="sql">sql脚本</param>
    /// <param name="sqlparms">参数数组</param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<T> QuerySqlSingleAsync<T>(string sql, object? sqlparms = null)
    {
        return await _client.Ado.SqlQuerySingleAsync<T>(sql, sqlparms);
    }

    /// <summary>
    /// sql脚本 获取查询列表数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="sqlparms"></param>
    /// <returns>返回影响行数</returns>
    public virtual async Task<List<T>> QuerySqlListAsync<T>(string sql, object? sqlparms = null)
    {
        return await _client.Ado.SqlQueryAsync<T>(sql, sqlparms);
    }

    /// <summary>
    /// SqlQueryable
    /// </summary>
    /// <typeparam name="T">动态实体对象</typeparam>
    /// <param name="sqlStr">sql脚本</param>
    /// <param name="parameters">参数</param>
    /// <returns>返回IQueryable对象</returns>
    public virtual IQueryable<T> SqlQueryable<T>(string sqlStr, params object[] parameters)
    {
        if (parameters != null && parameters.Length > 0)
            return _client.Ado.SqlQuery<T>(sqlStr, parameters).AsQueryable();
        return _client.Ado.SqlQuery<T>(sqlStr).AsQueryable();
    }

    /// <summary>
    /// SqlQueryable
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="sqlStr">sql脚本</param>
    /// <returns></returns>
    public virtual ISugarQueryable<T> SqlQueryable<T>(string sqlStr) where T : class, new()
    {
        return _client.SqlQueryable<T>(sqlStr);
    }

    /// <summary>
    /// SQL查询
    /// </summary>
    /// <typeparam name="T">动态实体</typeparam>
    /// <param name="predicate">表达式-筛选条件</param>
    /// <param name="sqlStr">sql脚本</param>
    /// <returns>返回List<T></returns>
    public virtual List<T> SqlQuery<T>(Expression<Func<T, bool>> predicate, string sqlStr)
    {
        var query = _client.Ado.SqlQuery<T>(sqlStr).AsQueryable();
        if (predicate != null)
            return query.Where(predicate).ToList();
        return query.ToList();
    }

    /// <summary>
    /// SQL查询
    /// </summary>
    /// <typeparam name="T">动态实体</typeparam>
    /// <param name="predicates">表达式-筛选条件</param>
    /// <param name="sqlStr">sql脚本</param>
    /// <returns>返回List<T></returns>
    public virtual List<T> SqlQuery<T>(List<Expression<Func<T, bool>>> predicates, string sqlStr)
    {
        IQueryable<T>? queryable = _client.Ado.SqlQuery<T>(sqlStr).AsQueryable();
        if (predicates != null && predicates.Any())
            return predicates.Aggregate(queryable, (current, p) => current.Where<T>(p)).ToList();
        return queryable.ToList();
    }

    #endregion sql脚本

    #region 存储过程

    /// <summary>
    /// 查询存储过程
    /// </summary> 
    /// <param name="procedureName">存储过程名称</param>
    /// <param name="parameters">参数</param>
    /// <returns>DataSet</returns>
    public virtual async Task<DataSet> QueryProcedureDataSet(string procedureName, List<SugarParameter> parameters)
    {
        DataSet? datas = await _client.Ado.UseStoredProcedure().GetDataSetAllAsync(procedureName, parameters);
        return datas;
    }

    /// <summary>
    /// 查询存储过程
    /// </summary> 
    /// <param name="procedureName">存储过程名称</param>
    /// <param name="parameters">参数</param>
    /// <returns>DataTable</returns>
    public virtual async Task<DataTable> QueryProcedureDataTable(string procedureName, List<SugarParameter> parameters)
    {
        DataTable? datas = await _client.Ado.UseStoredProcedure().GetDataTableAsync(procedureName, parameters);
        return datas;
    }

    /// <summary>
    /// 查询存储过程
    /// </summary> 
    /// <param name="procedureName">存储过程名称</param>
    /// <param name="parameters">参数</param>
    /// <returns>单个值</returns>
    public virtual async Task<object> QueryProcedureScalar(string procedureName, List<SugarParameter> parameters)
    {
        object? datas = await _client.Ado.UseStoredProcedure().GetScalarAsync(procedureName, parameters);
        return datas;
    }

    #endregion

    #region 分页查询-表达式

    /// <summary>
    /// 泛型分页查询 返回Valuetuple类型
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="whereExpression">筛选条件</param>
    /// <param name="pageIndex">第几页</param>
    /// <param name="pageSize">每页显示的数量</param>
    /// <param name="orderByFileds">排序条件</param>
    /// <returns></returns>
    public virtual async Task<(List<T> T, RefAsync<int> totalCount, int pageCount, int pageIndex, int pageSize)> QueryPageAsync<T>(Expression<Func<T, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, string? orderByFileds = null) where T : EntityBase, new()
    {
        RefAsync<int> totalCount = 0;
        List<T>? list = await _client.Queryable<T>().With(SqlWith.NoLock)
         .WhereIF(whereExpression != null, whereExpression)
         .OrderByIF(!string.IsNullOrEmpty(orderByFileds), orderByFileds)
         .ToPageListAsync(pageIndex, pageSize, totalCount);

        int pageCount = (int)(Math.Ceiling((totalCount * 1.0) / (pageSize * 1.0)));
        return (list, totalCount, pageCount, pageIndex, pageSize);
    }

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
    public virtual async Task<dynamic> GetGroupByPageListAsync<T>(Expression<Func<T, bool>> predicate, RefAsync<int> total, int pageIndex = 1, int pageSize = 10, Expression<Func<T, object>>? groupBys = null, Expression<Func<T, object>>? selects = null, Expression<Func<T, object>>? orderBy = null, bool desc = true) where T : EntityBase, new()
    {
        ISugarQueryable<T>? query = _client.Queryable<T>().With(SqlWith.NoLock).Where(predicate);
        if (orderBy != null)
        {
            query = desc ? query.OrderBy(orderBy, OrderByType.Desc) : query.OrderBy(orderBy, OrderByType.Asc);
        }
        else
        {
            query = query.OrderBy(m => m.CreatedTime, OrderByType.Desc);
        }
        if (groupBys != null)
        {
            return await query.GroupBy(groupBys).Select(selects).ToPageListAsync(pageIndex, pageSize, total);
        }
        return await query.ToPageListAsync(pageIndex, pageSize, total);
    }

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
    public virtual async Task<List<T>> PageListAsync<T>(int pageIndex, int pageSize, RefAsync<int> total, Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderBy = null, bool desc = true) where T : EntityBase, new()
    {
        ISugarQueryable<T>? query = _client.Queryable<T>().With(SqlWith.NoLock).Where(predicate);
        if (orderBy != null)
        {
            query = desc ? query.OrderBy(orderBy, OrderByType.Desc) : query.OrderBy(orderBy, OrderByType.Asc);
        }
        else
        {
            query = query.OrderBy(m => m.CreatedTime, OrderByType.Desc);
        }
        return await query.ToPageListAsync(pageIndex, pageSize, total);
    }

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
    /// <param name="orderBy">最内层orderby  例: table1.xxx desc,table2.ad asc "</param>
    /// <param name="pageIndex">页数</param>
    /// <param name="pageSize">每页条数</param>
    /// <param name="maxCount">最大获取数,当设置此值时Count返回不会超过这个限定值，设置该值可以提升查询效率，建意这个值在10W以内</param>
    /// <returns>列表</returns>
    public virtual IList<T>? GetPageList<T>(out int count, string table, string colList, string whereStr, string orderBy, int pageIndex, int pageSize = 10, int maxCount = 0) where T : new()
    {
        string? where = string.IsNullOrEmpty(whereStr) ? " " : " where (" + whereStr + ") ";
        string? sql = "select count(1) from " + table + where;
        count = _client.Ado.GetInt(sql);
        if (maxCount > 0 && count > maxCount) { count = maxCount; }
        if (pageIndex <= 1)
        {
            string? limit = "limit " + (maxCount > 0 && pageSize > maxCount ? maxCount : pageSize) + " ";
            sql = "select " + colList + " from " + table + where + " " + orderBy + " " + limit;
        }
        else
        {
            int start = pageSize * (pageIndex - 1), end = pageSize * pageIndex;
            if (maxCount > 0) if (start > maxCount) { end = 0; } else if (end > maxCount) { end = maxCount; }
            sql = "select * from (select " + colList + ", row_number() over(" + orderBy + ") as rownumber from " + table + where + ") pg where rownumber>" + start + " and rownumber<=" + end + " order by rownumber asc";
        }
        DataTable? dt = _client.Ado.GetDataTable(sql);
        return XBoot.Composables.XBootExtensions.ConvertToModel<T>(dt);
    }

    /// <summary>
    /// 分页读取列表
    /// </summary>
    /// <typeparam name="T">分页信息类</typeparam>
    /// <param name="table">表名 例： table1 inner join table2 on table1.xx=table2.xx</param>
    /// <param name="colList">需要获取字段 例: tabl1.xx,table2.*,注意，需要把排序列都选上</param>
    /// <param name="where">条件,不带where</param>
    /// <param name="orderBy">最内层orderby  例: table1.xxx desc,table2.ad asc "</param>
    /// <param name="pageIndex">页数</param>
    /// <param name="pageSize">每页条数</param>
    /// <param name="MaxCount">最大获取数,当设置此值时Count返回不会超过这个限定值，设置该值可以提升查询效率，建意这个值在10W以内</param>
    /// <returns>元组：IList,总数</returns>
    public virtual async Task<(IList<T>? list, RefAsync<int> totalCount)> PageListAsync<T>(string table, string colList, string where, string orderBy, int pageIndex, int pageSize = 10, int MaxCount = 0) where T : new()
    {
        (DataTable result, RefAsync<int> count) tuples = await PageListTable(table, colList, where, orderBy, pageIndex, pageSize, MaxCount);
        RefAsync<int> count = tuples.count;
        IList<T>? lst = XBoot.Composables.XBootExtensions.ConvertToModel<T>(tuples.result);
        return (lst, count);
    }

    /// <summary>
    /// 分页读取列表
    /// </summary>
    /// <param name="table">表名 例： table1 inner join table2 on table1.xx=table2.xx</param>
    /// <param name="colList">需要获取字段 例: tabl1.xx,table2.*,注意，需要把排序列都选上</param>
    /// <param name="whereStr">条件,不带where</param>
    /// <param name="orderBy">最内层orderby  例: table1.xxx desc,table2.ad asc "</param>
    /// <param name="pageIndex">页数</param>
    /// <param name="pageSize">每页条数</param>
    /// <param name="maxCount">最大获取数,当设置此值时Count返回不会超过这个限定值，设置该值可以提升查询效率，建意这个值在10W以内</param>
    /// <returns>元组：DataTable,总数</returns>
    public virtual async Task<(DataTable result, RefAsync<int> count)> PageListTable(string table, string colList, string whereStr, string orderBy, int pageIndex, int pageSize = 10, int maxCount = 0)
    {
        string? where = string.IsNullOrEmpty(whereStr) ? " " : " where (" + whereStr + ") ";
        string? sql = "select count(1) from " + table + where;
        RefAsync<int> count = await _client.Ado.GetIntAsync(sql);
        if (maxCount > 0 && count > maxCount) { count = maxCount; }

        if (pageIndex <= 1)
        {
            string? limit = "limit " + (maxCount > 0 && pageSize > maxCount ? maxCount : pageSize) + " ";
            sql = "select " + colList + " from " + table + where + " " + orderBy + " " + limit;
        }
        else
        {
            int start = pageSize * (pageIndex - 1), end = pageSize * pageIndex;
            if (maxCount > 0) if (start > maxCount) { end = 0; } else if (end > maxCount) { end = maxCount; }
            sql = "select * from (select " + colList + ", row_number() over(" + orderBy + ") as rownumber from " + table + where + ") pg where rownumber>" + start + " and rownumber<=" + end + " order by rownumber asc";
        }
        DataTable? result = await _client.Ado.GetDataTableAsync(sql);
        return (result, count);
    }

    /// <summary>
    /// 获取数据集
    /// </summary>
    /// <param name="sql">sql脚本</param>
    /// <param name="parameters"></param>
    /// <returns>返回DataTable</returns>
    public virtual async Task<DataTable> GetDataTableAsync(string sql, params SugarParameter[] parameters)
    {
        return await _client.Ado.GetDataTableAsync(sql, parameters);
    }

    /// <summary>
    /// 获取数据集
    /// </summary>
    /// <param name="sql">sql脚本</param>
    /// <param name="parameters"></param>
    /// <returns>返回DataTable</returns>
    public virtual async Task<DataTable> GetDataTableAsync(string sql, List<SugarParameter> parameters)
    {
        return await _client.Ado.GetDataTableAsync(sql, parameters);
    }

    #endregion 分页查询 之 sql脚本

    #region SqlBulkCopy

    /// <summary>
    /// 并发高用异步方法吐能量能提高,
    /// 一次插入的数据量越大他的价值越高，比如1000以下就不合算了
    /// ，数据量小的时候并发吞吐量不如普通插入
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="lstData"></param>
    /// <returns>返回影响的行数</returns>
    public virtual async Task<int> InsertBulkCopyAsync<T>(List<T> lstData) where T : EntityBase, new()
    {
        int reslut = await _client.Fastest<T>().BulkCopyAsync(lstData);
        return reslut;
    }

    /// <summary>
    /// 并发高用异步方法吐能量能提高,
    /// 一次插入的数据量越大他的价值越高，比如1000以下就不合算了
    /// ，数据量小的时候并发吞吐量不如普通插入
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="lstData"></param>
    /// <returns>返回影响的行数</returns>
    public virtual async Task<int> UpdateBulkCopyAsync<T>(List<T> lstData) where T : EntityBase, new()
    {
        int reslut = await _client.Fastest<T>().BulkUpdateAsync(lstData);
        return reslut;
    }

    /// <summary>
    /// 同时插入及更新
    /// </summary>
    /// <typeparam name="T">DB模型</typeparam>
    /// <param name="lstData"></param>
    /// <param name="columns">非Id的自定义主键</param>
    /// <param name="ignoreColumns">忽略字段集合</param>
    /// <returns>返回影响的行数ValueTuple</returns>
    public virtual async Task<(int insert, int update)> AddOrUpdateAsync<T>(List<T> lstData, Expression<Func<T, object>>? columns = null, string[]? ignoreColumns = null) where T : EntityBase, new()
    {
        StorageableResult<T>? x;
        if (columns != null)
        {
            x = _client.Storageable(lstData).WhereColumns(columns).ToStorage();
        }
        else
        {
            x = _client.Storageable(lstData).ToStorage();
        }

        //var insertInt = await x.AsInsertable.IgnoreColumns("id").ExecuteCommandAsync();//不存在插入
        var isIgnoreColumnID = lstData.Any(a => string.IsNullOrEmpty(a.Id.ToString()));//是否存在Id为0的数据

        var insertInt = 0;

        if (isIgnoreColumnID)
        {
            insertInt = await x.AsInsertable.IgnoreColumns("id").ExecuteCommandAsync();//不存在插入
        }
        else
        {
            insertInt = await x.BulkCopyAsync(); //不存在插入
        }
        var upInt = await x.BulkUpdateAsync();//存在更新
        return (insertInt, upInt);
    }

    public virtual async Task<int> InsertListCreatedIdAsync<T>(List<T> lstData) where T : EntityBase, new()
    {
        lstData.ForEach(item => item.Id = this.CreateNewGuid());
        int reslut = await _client.Fastest<T>().BulkCopyAsync(lstData);
        return reslut;
    }
    #endregion SqlBulkCopy
}

