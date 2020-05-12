using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using DES = Encrypts.DES;

namespace My.Videos.Common.Utils
{
    public class ManagePass
    {
        /// <summary>
        /// Encrypt dll锁
        /// </summary>        
        private static readonly Dld DecryptDld = new Dld();

        /// <summary>
        /// 使用指定密钥解密
        /// </summary>
        /// <param name="original">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, Encoding.Default);
        }

        /// <summary>
        /// 使用指定密钥解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>明文</returns>
        private static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = Encoding.Default.GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }

        /// <summary>
        /// 使用指定密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        private static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider
            {
                Key = MakeMd5(key),
                Mode = CipherMode.ECB
            };

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        //----------------------------------------------------------------------------------------

        /// <summary>
        /// 使用指定密钥加密
        /// </summary>
        /// <param name="original">需要加密的字符串</param>
        /// <param name="key">自定义的密钥</param>
        /// <returns>返回加密字符串</returns>
        public static string Encrypt(string original, string key)
        {
            byte[] buff = Encoding.Default.GetBytes(original);
            byte[] kb = Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }

        /// <summary>
        /// 使用指定密钥加密
        /// </summary>
        /// <param name="original">需要加密的字符串</param>
        /// <param name="key">自定义的密钥</param>
        /// <returns>返回加密字符串</returns>
        private static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider
            {
                Key = MakeMd5(key),
                Mode = CipherMode.ECB
            };

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }


        //----------------------------------------------------------------------------------------

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <returns>摘要</returns>
        private static byte[] MakeMd5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            return keyhash;
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string WebServiceEncrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            var inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string WebServiceDecrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            var len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x;
            for (x = 0; x < len; x++)
            {
                var i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// DEC解密方法
        /// </summary>
        /// <param name="original"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptDec(string original, string key)
        {
            string str = string.Empty;

            try
            {
                lock (DecryptDld)
                {

                    if (!DecryptDld.BLoadDll)
                    {
                        string dllPath = string.Format(@"{0}dll\UrlParam.dll", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

                        DecryptDld.LoadDll(dllPath);
                    }
                    DecryptDld.LoadFun("UrlDecoding");

                    object[] parameters = new object[] { key, original }; // 实参为0

                    Type[] parameterTypes = new Type[] { typeof(string), typeof(string) }; // 实参类型为int

                    ModePass[] themode = new ModePass[] { ModePass.ByValue, ModePass.ByValue }; // 传送方式为值传

                    Type typeReturn = typeof(IntPtr); // 返回类型为int


                    IntPtr ret = (IntPtr)DecryptDld.Invoke(parameters, parameterTypes, themode, typeReturn);
                    str = Marshal.PtrToStringAnsi(ret);

                    //myDLD.UnLoadDll();

                    Marshal.Release(ret);
                }
            }
            catch (Exception)
            {
                //  LogWriter.Write("PASS", err.Message);
            }
            return str;
        }

        /// <summary>
        /// DEC加密方法
        /// </summary>
        /// <param name="original"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptDec(string original, string key)
        {
            string str = string.Empty;

            try
            {
                lock (DecryptDld)
                {

                    if (!DecryptDld.BLoadDll)
                    {
                        string dllPath = string.Format(@"{0}dll\UrlParam.dll", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

                        DecryptDld.LoadDll(dllPath);
                    }
                    DecryptDld.LoadFun("UrlEncoding");

                    object[] parameters = new object[] { key, original }; // 实参为0

                    Type[] parameterTypes = new Type[] { typeof(string), typeof(string) }; // 实参类型为int

                    ModePass[] themode = new ModePass[] { ModePass.ByValue, ModePass.ByValue }; // 传送方式为值传

                    Type typeReturn = typeof(IntPtr); // 返回类型为int


                    IntPtr ret = (IntPtr)DecryptDld.Invoke(parameters, parameterTypes, themode, typeReturn);
                    str = Marshal.PtrToStringAnsi(ret);

                    //DecryptDLD.UnLoadDll();

                    Marshal.Release(ret);
                }
            }
            catch (Exception)
            {
                //  LogWriter.Write("PASS", err.Message);
            }

            return str;
        }

        /// <summary>
        /// DES加密方法
        /// </summary>
        /// <param name="original">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static string EncryptDes(string original, string key)
        {
            if (key.Length < 8)
                throw new Exception("密钥长度不能小于8位");

            byte[] keys = Encoding.Default.GetBytes(key);
            byte[] data = Encoding.Default.GetBytes(original);
            DES des = new DES(keys);
            byte[] encrypt = des.encrypt(data, 0);
            return Convert.ToBase64String(encrypt);
        }

        /// <summary>
        /// DES解密方法
        /// </summary>
        /// <param name="original">要解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string DecryptDes(string original, string key)
        {
            if (key.Length < 8)
                throw new Exception("密钥长度不能小于8位");

            byte[] keys = Encoding.Default.GetBytes(key);
            byte[] data = Convert.FromBase64String(original);
            DES des = new DES(keys);
            byte[] decrypt = des.decrypt(data, 0);
            return Encoding.Default.GetString(decrypt);
        }
    }
    /// <summary>
    /// ModePass
    /// </summary>
    public enum ModePass
    {

        /// <summary>
        /// ByValue
        /// </summary>
        ByValue = 0x0001,

        /// <summary>
        /// ByValue
        /// </summary>
        ByRef = 0x0002

    }
    /// <summary>
    /// Dld
    /// </summary>
    public class Dld
    {
        /// <summary>
        /// LoadLibrary
        /// </summary>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]

        public static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary>
        /// GetProcAddress
        /// </summary>
        /// <param name="hModule"></param>
        /// <param name="lpProceName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProceName);

        /// <summary>
        /// FreeLibrary
        /// </summary>
        /// <param name="hModule"></param>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// 是否载入dll
        /// </summary>
        public bool BLoadDll
        {
            get
            {
                return !(_hModule == IntPtr.Zero);
            }
        }

        /// <summary>      
        /// Loadlibrary 返回的函数库模块的句柄        
        /// </summary>   
        private IntPtr _hModule = IntPtr.Zero;

        /// <summary>        
        /// GetProcAddress 返回的函数指针    
        /// </summary>       
        private IntPtr _farProc = IntPtr.Zero;

        /// <summary>       
        /// 装载 Dll       
        /// </summary>     
        /// <param name="lpFileName">DLL 文件名 </param>  
        public void LoadDll(string lpFileName)
        {
            _hModule = LoadLibrary(lpFileName);
            if (_hModule == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpFileName + "."));
        }
        /// <summary>      
        /// 获得函数指针      
        /// </summary>    
        /// <param name="lpProcName"> 调用函数的名称 </param>        
        public void LoadFun(string lpProcName)
        { // 若函数库模块的句柄为空，则抛出异常

            if (_hModule == IntPtr.Zero)
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            // 取得函数指针   
            _farProc = GetProcAddress(_hModule, lpProcName);
            // 若函数指针，则抛出异常   
            if (_farProc == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpProcName + " 这个函数的入口点 "));
        }

        /// <summary>       
        /// 卸载 Dll       
        /// </summary>       
        public void UnLoadDll()
        {
            FreeLibrary(_hModule);
            _hModule = IntPtr.Zero;
            _farProc = IntPtr.Zero;
        }

        /// <summary>      
        /// 调用所设定的函数     
        /// </summary>       
        /// <param name="objArrayParameter"> 实参 </param>  
        /// <param name="typeArrayParameterType"> 实参类型 </param>    
        /// <param name="modePassArrayParameter"> 实参传送方式 </param>    
        /// <param name="typeReturn"> 返回类型 </param>      
        /// <returns> 返回所调用函数的 object</returns>
        public object Invoke(object[] objArrayParameter, Type[] typeArrayParameterType, ModePass[] modePassArrayParameter, Type typeReturn)
        {
            // 下面 3 个 if 是进行安全检查 , 若不能通过 , 则抛出异常    
            if (_hModule == IntPtr.Zero)
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            if (_farProc == IntPtr.Zero)
                throw (new Exception(" 函数指针为空 , 请确保已进行 LoadFun 操作 !"));
            if (objArrayParameter.Length != modePassArrayParameter.Length)
                throw (new Exception(" 参数个数及其传递方式的个数不匹配 ."));
            // 下面是创建 MyAssemblyName 对象并设置其 Name 属性          
            AssemblyName myAssemblyName = new AssemblyName { Name = "InvokeFun" };
            // 生成单模块配件              
            AssemblyBuilder myAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(myAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder myModuleBuilder = myAssemblyBuilder.DefineDynamicModule("InvokeDll");
            // 定义要调用的方法 , 方法名为“ MyFun ”，返回类型是“ Type_Return ”参数类型是“ TypeArray_ParameterType ”



            MethodBuilder myMethodBuilder = myModuleBuilder.DefineGlobalMethod("MyFun", MethodAttributes.Public | MethodAttributes.Static, typeReturn, typeArrayParameterType);



            // 获取一个 ILGenerator ，用于发送所需的 IL



            ILGenerator il = myMethodBuilder.GetILGenerator();



            int i;



            for (i = 0; i < objArrayParameter.Length; i++)
            {// 用循环将参数依次压入堆栈



                switch (modePassArrayParameter[i])
                {



                    case ModePass.ByValue:



                        il.Emit(OpCodes.Ldarg, i);



                        break;



                    case ModePass.ByRef:



                        il.Emit(OpCodes.Ldarga, i);



                        break;



                    default:



                        throw (new Exception(" 第 " + (i + 1).ToString() + " 个参数没有给定正确的传递方式 ."));



                }



            }



            if (IntPtr.Size == 4)
            {// 判断处理器类型



                il.Emit(OpCodes.Ldc_I4, _farProc.ToInt32());



            }



            else if (IntPtr.Size == 8)
            {



                il.Emit(OpCodes.Ldc_I8, _farProc.ToInt64());



            }



            else
            {



                throw new PlatformNotSupportedException();



            }



            il.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, typeReturn, typeArrayParameterType);



            il.Emit(OpCodes.Ret); // 返回值



            myModuleBuilder.CreateGlobalFunctions();



            // 取得方法信息



            MethodInfo myMethodInfo = myModuleBuilder.GetMethod("MyFun");



            return myMethodInfo.Invoke(null, objArrayParameter);// 调用方法，并返回其值



        }
    }
}
