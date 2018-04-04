using Simple.Dapper.Demo.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;


namespace Simple.Dapper.Demo
{
    /*
    * Dapper SimpleCRUD api 使用
    * */
    public class b_crud : b_base
    {

        #region 手写Sql插入数据
        /// <summary>
        /// 手写Sql插入数据
        /// </summary>
        public int InsertWithSql()
        {
            using (var conn = Connection)
            {
                string _sql = "INSERT INTO t_department(departmentname,introduce,[enable])VALUES('应用开发部SQL','应用开发部主要开始公司的应用平台',1)";
                conn.Open();
                return conn.Execute(_sql);
            }
        }
        #endregion

        #region 实体插入数据
        /// <summary>
        /// 实体插入数据
        /// </summary>
        public int? InsertWithEntity()
        {
            using (var conn = Connection)
            {
                var _entity = new t_department { departmentname = "应用开发部ENTITY", introduce = "应用开发部主要开始公司的应用平台" };
                conn.Open();
                return conn.Insert(_entity);
            }
        }
        #endregion

        #region 批量插入数据方法一
        public void InsertDataBatch()
        {
            using (var conn = Connection)
            {
                conn.Open();
                var r = conn.Execute(@"insert Person(username, password,age,registerDate,address) values (@a, @b,@c,@d,@e)",
                new[] { 
			        new { a = 1, b = 1, c = 1, d = DateTime.Now, e = 1 }
			        , new { a = 2, b = 2, c = 2, d = DateTime.Now, e = 2 }
			        , new { a = 3, b = 3, c = 3, d = DateTime.Now, e = 3 } 
		        });

                conn.Close();
            }
        }

        #endregion

        #region 批量插入数据方法二
        /// <summary>
        /// 批量插入数据方法二
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="entities"></param>
        public void InsertMultiple<T>(string sql, IEnumerable<T> entities) where T : class, new()
        {
            using (var cnn = Connection)
            {
                //int records = 0;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        cnn.Execute(sql, entities, trans, 30, CommandType.Text);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                //foreach (T entity in entities)
                //{
                //    records += cnn.Execute(sql, entity);
                //}
                //return records;
            }
        }

        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        public void UpdateData()
        {
            using (var conn = Connection)
            {
                conn.Open();
                var r = conn.Execute(@"update Person set password='www.lanhuseo.com' where username=@username", new { username = 2 });
                conn.Close();
            }
        }

        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        public void RemoveData()
        {
            using (var conn = Connection)
            {
                conn.Open();
                var r = conn.Execute(@"delete from Person where id=@id", new { id = 2 });
                conn.Close();
            }
        }

        #endregion

        #region 在IDBconnection中使用事务
        /// <summary>
        /// 在IDBconnection中使用事务
        /// </summary>
        /// <returns></returns>
        public bool InsertWithTran()
        {
            using (var conn = Connection)
            {
                int _departmentid = 0, _employeeid = 0, _rnum = 0;
                var _departmentname = new t_department { departmentname = "应用开发部ENTITY", introduce = "应用开发部主要开始公司的应用平台" };
                var _employee = new t_employee { displayname = "Micro", email = "1441299@qq.com", loginname = "Micro", password = "66778899", mobile = "123456789" };
                conn.Open();
                var _tran = conn.BeginTransaction();
                try
                {
                    _departmentid = conn.Insert(_departmentname, transaction: _tran).Value;
                    ++_rnum;
                    _employeeid = conn.Insert(_employee, transaction: _tran).Value;
                    ++_rnum;
                    conn.Insert(new t_derelation { departmentid = _departmentid, employeeid = _employeeid }, transaction: _tran);
                    ++_rnum;
                    _tran.Commit();
                }
                catch
                {
                    _rnum = 0;
                    _tran.Rollback();
                }
                return _rnum > 0;
            }
        }
        #endregion

        #region 在存储过程中使用事务
        /// <summary>
        /// 在存储过程中使用事务
        /// </summary>
        /// <returns></returns>
        public bool InsertWithProcTran()
        {
            var _parameter = new DynamicParameters();
            _parameter.Add("departmentname", "外网开发部门");
            _parameter.Add("introduce", "外网开发部门负责外部网站的更新");
            _parameter.Add("displayname", "夏季冰点");
            _parameter.Add("loginname", "Micro");
            _parameter.Add("password", "123456789");
            _parameter.Add("mobile", "1122334455");
            _parameter.Add("email", "123456789@qq.com");
            using (var _conn = Connection)
            {
                _conn.Open();
                return
                    _conn.Query<bool>("p_Insertdata", _parameter, commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
            }
        }
        #endregion

        #region 查询所有员工信息方法一
        /// <summary>
        /// 查询所有员工信息方法一
        /// </summary>
        /// <returns></returns>
        public IEnumerable<t_employee> GetemployeeListFirst()
        {
            string _sql = "SELECT * FROM t_employee";
            using (var _conn = Connection)
            {
                _conn.Open();
                return _conn.Query<t_employee>(_sql);
            }
        }
        #endregion

        #region 查询所有员工信息方法二
        /// <summary>
        /// 查询所有员工信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<t_employee> GetemployeeListSecond()
        {
            using (var _conn = Connection)
            {
                _conn.Open();
                return _conn.GetList<t_employee>();
            }
        }
        #endregion

        #region 获取某位员工的信息方法一
        /// <summary>
        /// 获取某位员工的信息方法一
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        public t_employee GetemployeeFirst(int employeeid)
        {
            string _sql = "SELECT * FROM t_employee where employeeid=@pemployeeid";
            using (var _conn = Connection)
            {
                _conn.Open();
                return _conn.Query<t_employee>(_sql, new { pemployeeid = employeeid }).FirstOrDefault();
            }
        }
        #endregion

        #region 获取某位员工的信息方法二
        /// <summary>
        /// 获取某位员工的信息方法二
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        public t_employee GetemployeetSecond(int employeeid)
        {
            using (var _conn = Connection)
            {
                _conn.Open();
                return _conn.Get<t_employee>(employeeid);
            }
        }
        #endregion

        #region 获取某位员工的信息方法三
        /// <summary>
        /// 获取某位员工的信息方法三
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        public t_employee Getemployeethird(int pemployeeid)
        {
            using (var _conn = Connection)
            {
                _conn.Open();
                return _conn.GetList<t_employee>(new { employeeid = pemployeeid }).FirstOrDefault();
            }
        }
        #endregion

        #region 多表查询(获取部门&员工信息)
        /// <summary>
        /// 多表查询(获取部门&员工信息)
        /// </summary>
        public void GetMultiEntity()
        {
            string _sql = "SELECT * FROM t_department AS a;SELECT * FROM t_employee AS a";
            using (var _conn = Connection)
            {
                var _grid = _conn.QueryMultiple(_sql);
                var _department = _grid.Read<t_department>();
                var _employee = _grid.Read<t_employee>();
            }
        }
        #endregion

        #region 父子关系查询
        /// <summary>
        /// 父子关系查询
        /// </summary>
        public IEnumerable<t_department> GetPCEntity()
        {
            string _sql = "SELECT * FROM t_department AS a;SELECT * FROM t_employee AS a;SELECT * FROM t_derelation;";
            using (var _conn = Connection)
            {
                var _grid = _conn.QueryMultiple(_sql);
                var _department = _grid.Read<t_department>();
                var _employee = _grid.Read<t_employee>();
                var _derelation = _grid.Read<t_derelation>();
                foreach (var tDepartment in _department)
                {
                    tDepartment.ListEmployees = _employee.Join(_derelation.Where(v => v.departmentid == tDepartment.departmentid), p => p.employeeid, r => r.employeeid, (p, r) => p).ToList();
                }
                return _department;
            }
        }
        #endregion

        #region * join etc. 的使用
        /// <summary>
        /// * join的使用 多对多映射 （不太好解决，可以考虑使用存储过程）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<t_department> GetDepartment()
        {
            string _sql = "select d.*, e.* from t_department d left join t_derelation r on d.departmentid = r.departmentid left join t_employee e on r.employeeid = e.employeeid";
            using (var _conn = Connection)
            {
                _conn.Open();
                var list = _conn.Query<t_department, t_derelation, t_employee, t_department>(_sql, (d, r, e) =>
                {
                    d.ListEmployees.Add(e);
                    return d;
                }, splitOn: "");

                return list;
            }
        }

        /// <summary>  
        /// * join的使用 一对一映射  
        /// 订单实体下有个用户信息属性，通过user_id关联  
        /// </summary>  
        public void TestOneToOne()
        {
            //using (IDbConnection conn = Connection)
            //{
            //    var sql = @"SELECT a.*,b.*   
            //                            FROM   
            //                             order_info a  
            //                             JOIN user_info b  
            //                             ON a.`user_id`=b.user_id;";
            //    var list = conn.Query<order_infoModel, user_infoModel, order_infoModel>(
            //        sql
            //        , (order, user) =>
            //        {
            //            order.userInfo = user;
            //            return order;
            //        }
            //        , null
            //        , null
            //        , true
            //        , "order_id"
            //        , null
            //        , null).ToList();
            //}
        }

        /// <summary>  
        /// 一对多映射  
        /// </summary>  
        public void TestOneToMore()
        {
            using (IDbConnection conn = Connection)
            {
                var sql = @"SELECT a.*,b.*   
                        FROM order_info a  
                         JOIN order_item b ON a.order_id=b.order_id;";
                //合并后的订单数据  
                //var orderDic = new Dictionary<int, order_infoModel>();
                //var originList = conn.Query<order_infoModel, order_itemModel, order_infoModel>(
                //    sql
                //    , (order, goods) =>
                //    {
                //        //需要手动维护，一对多对象关系  
                //        //order_infoModel ord;
                //        //if (!orderDic.TryGetValue(order.order_id, out ord))
                //        //{
                //        //    ord = order; //其实就是将goods对象赋值到order对象引用指向的对象的属性
                //        //    orderDic.Add(order.order_id, order);
                //        //}
                //        order.goodsList.Add(goods);
                //        return order;
                //    }
                //    , null
                //    , null
                //    , true
                //    , "order_id"
                //    , null
                //    , null).ToList();
                //投影一个list  
                //var list = orderDic.Select(x => x.Value).ToList();

                //return list;
            }
        }

        #endregion

        #region 简单分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pstart"></param>
        /// <param name="pend"></param>
        /// <returns></returns>
        public IEnumerable<t_employee> GetPaging(int pstart = 0, int pend = 5)
        {
            string _sql = "SELECT * FROM (SELECT a.*, ROW_NUMBER() OVER (ORDER BY a.employeeid) rownum FROM t_employee as a ) b WHERE b.rownum BETWEEN @start AND @end ORDER BY b.rownum";
            using (var _conn = Connection)
            {
                return _conn.Query<t_employee>(_sql, new { start = pstart, end = pend });
            }
        }
        #endregion

        #region 通用分页
        /// <summary>
        /// 通用分页
        /// </summary>
        /// <returns></returns>
        public int GetPaging()
        {
            ////实际开发可以独立出来处理/////////////
            var _ppaging = new p_PageList<t_employee>();
            _ppaging.Tables = "t_employee";
            _ppaging.OrderFields = "employeeid asc";
            ///////////////////////////////////////
            var _dy = new DynamicParameters();
            _dy.Add("Tables", _ppaging.Tables);
            _dy.Add("OrderFields", _ppaging.OrderFields);
            _dy.Add("TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var _conn = Connection)
            {
                _conn.Open();
                _ppaging.DataList = _conn.Query<t_employee>("p_PageList", _dy, commandType: CommandType.StoredProcedure);
            }
            _ppaging.TotalCount = _dy.Get<int>("TotalCount");
            return _ppaging.PageCount;
        }
        #endregion

        #region 存储过程Demo
        /// <summary>
        /// 存储过程Demo
        /// </summary>
        public Tuple<string, string> ProceDemo()
        {
            int employeeid = 1;
            var _mobile = "";
            var _dy = new DynamicParameters();
            _dy.Add("employeeid", employeeid);
            _dy.Add("displayname", string.Empty, dbType: DbType.String, direction: ParameterDirection.Output);
            using (var _conn = Connection)
            {
                _conn.Open();
                _mobile = _conn.Query<string>("p_Procedemo", _dy, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return Tuple.Create(_mobile, _dy.Get<string>("displayname"));
        }
        #endregion
    }
}
