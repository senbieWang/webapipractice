using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace WebApiStandardTest.V1.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.1")]
    [ApiVersion("1.0",Deprecated = true)] 
    [Route("bims/v{version:apiVersion}/subdatas")]
    [ApiController]
    
    public class SubdatasController : ControllerBase
    {
        /// <summary>
        /// 获取所有项目子集
        /// </summary>
        /// <returns></returns>
        [MapToApiVersion("1.1")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSubDatas()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 创建一个项目子集
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subdatename">项目子集名称</param>
        [MapToApiVersion("1.1")]
        [Route("{subdatename}")]
        [HttpPost]
        
        public void CreateSubData(string value, string subdatename)
        {

        }

        /// <summary>
        /// 删除特定的数据子集
        /// </summary>
        /// <param name="subdataid">项目子集名称</param>
        [MapToApiVersion("1.0")]
        [Route("{subdataid}")]
        [HttpDelete()]
        public void DeleteSubData(string subdataid)
        {

        }

        /// <summary>
        /// 获取特定的项目子集
        /// </summary>
        /// <param name="subdataid">项目子集名称</param>
        /// <returns></returns>
        [Route("{subdataid}")]
        [HttpGet()]
        public ActionResult<string> GetSubDataByID(string subdataid)
        {
            return "value";
        }

        /// <summary>
        /// 给特定的项目子集上传/更新数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subdatename">项目子集名称</param>
        [Route("{subdataid}/bimsdata")]
        [HttpPost]
        public void CreateBimsDataforSubdata( string value, string subdatename)
        {

        }

        /// <summary>
        /// 给特定的项目子集创建组织关系
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="orgname">组织关系名</param>
        /// <returns></returns>
        [Route("{subdataid}/organizations/{orgname}")]
        [HttpPost()]
        public ActionResult<string> CreateOrganizations( string value, string subdataid, string orgname)
        {
            return " Organizations";
        }

        /// <summary>
        /// 获取特定项目子集的特定名称的组织关系数据
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="orgname">组织关系名</param>
        /// <returns></returns>
        [Route("{subdataid}/organizations/{orgname}")]
        [HttpGet()]
        public ActionResult<string> GetOrganizations( string subdataid, string orgname)
        {
            return "Organizations";
        }

        /// <summary>
        /// 给特定的项目子集的特定的组织关系添加组织关系数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="orgname">组织关系名</param>
        /// <returns></returns>
        [Route("{subdataid}/organizations/{orgname}/organizationdata")]
        [HttpPost()]
        public ActionResult<string> CreateOrganizationdataFororganization( string value, string subdataid, string orgname)
        {
            return " Organizations";
        }

        /// <summary>
        /// 获取特定项目子集的属性集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">数据版本号</param>
        /// <returns></returns>
        [Route("{subdataid}/bimsobjects")]
        [HttpGet()]
        public ActionResult<string> GetAttrbuties( string subdataid , int versionNo )
        {
            return "bimsobjects";
        }

        /// <summary>
        /// 获取特定项目子集特定构件的属性，支持过滤查询
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="bimsobjectid">构件ID</param>
        ///  <param name="versionNo">数据版本号，默认为当前最新版本</param>
        /// <param name="query">查询条件，非必须</param>
        /// <returns></returns>
        [Route("{subdataid}/bimsobjects/{bimsobjectid}")]
        [HttpGet()]
        public ActionResult<string> GetAttrbutie(string subdataid,  string bimsobjectid,  int versionNo, string query)
        {
            return "bimsobjects";
        }


        /// <summary>
        /// 获取特定项目子集的几何集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param> 
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid}/geomtrys")]
        [HttpGet()]
        public ActionResult<string> GetGeomtrys(string subdataid, int versionNo)
        {
            return "geomtrys";
        }

        /// <summary>
        /// 获取特定项目子集的材质集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid}/materials")]
        [HttpGet()]
        public ActionResult<string> GetMaterials( string subdataid, int versionNo)
        {
            return "materials";
        }

        /// <summary>
        /// 获取特定项目子集的贴图集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid}/textures")]
        [HttpGet()]
        public ActionResult<string> GetTextures( string subdataid, int versionNo)
        {
            return "textures";
        }

        /// <summary>
        /// 获取特定项目子集的管线管件集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid}/pipes")]
        [HttpGet()]
        public ActionResult<string> GetPipes( string subdataid,  int versionNo)
        {
            return "pipes";
        }

        /// <summary>
        /// 获取特定项目子集的空间集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid}/spaces")]
        [HttpGet()]
        public ActionResult<string> GetSpaces( string subdataid, int versionNo)
        {
            return "spaces";
        }



    }
}
