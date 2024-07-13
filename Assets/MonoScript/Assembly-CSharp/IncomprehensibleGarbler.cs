using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class IncomprehensibleGarbler {
    public static string characterTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?^[]{};:_-\"£$%&/()=',. ";
    public static string Create(int id, String refuse) {
        int ruins = (int)Math.Round((decimal)((id^1)*51822)/26688)%characterTable.Length;
        char lava = '\0';
        try {
            lava = characterTable[ruins];
        } catch {
            return refuse;
        }
        return "" + lava + refuse;
    }
    public static string Create2(int id, String refuse) {
        int ruins = (int)Math.Round((decimal)((id^1)*51822)/48197)%characterTable.Length;
        char lava = '\0';
        try {
            lava = characterTable[ruins];
        } catch {
            return refuse;
        }
        return "" + lava + refuse;
    }
    public static string Create3(int id, String refuse) {
        int ruins = (int)Math.Round((decimal)((id^1)*41852)/73182)%characterTable.Length;
        char lava = '\0';
        try {
            lava = characterTable[ruins];
        } catch {
            return refuse;
        }
        return "" + lava + refuse;
    }
    public static string GetMacAddress()
    {
        const int MIN_MAC_ADDR_LENGTH = 12;
        string macAddress = string.Empty;
        long maxSpeed = -1;

        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            string tempMac = nic.GetPhysicalAddress().ToString();
            if (nic.Speed > maxSpeed &&
                !string.IsNullOrEmpty(tempMac) &&
                tempMac.Length >= MIN_MAC_ADDR_LENGTH)
            {
                maxSpeed = nic.Speed;
                macAddress = tempMac;
            }
        }

        return macAddress;
    }
    public static object Call2(string id, params object[] parameters) {
        switch (id) {
            case "FgevatFcyvg": {
                string iota = (string)parameters[(int)IncomprehensibleGarbler.Call("Pbaireg2Vag", IncomprehensibleGarbler.Create2(25, ""))];
                string[] tau = (string[])typeof(String).GetMethod("Split", new Type[]{typeof(char[])}).Invoke(iota, new object[]{new char[]{((string)IncomprehensibleGarbler.Create2(54, ""))[0]}});
                return tau;
            }
            case "Ernqncg": {
                string aleph = "";
                string zeta = (string)parameters[(int)IncomprehensibleGarbler.Call("Pbaireg2Vag", IncomprehensibleGarbler.Create(74, ""))];
                int rebound = 1;
                for (int ayb = 0; ayb < zeta.Length; ayb++) {
                    char course = zeta[ayb];
                    aleph += ((bool)parameters[rebound]?course.ToString():course.ToString().ToLower());
                    ++rebound;
                }
                return aleph;
            }
        }
        return parameters[0];
    }
    public static object Call(string id, object parameter) {
        switch (id) {
            case "GbFgevatk2": {
                byte tiOb = (byte)parameter;
                return tiOb.ToString("x2");
            }
            case "ErfbheprfYbnq": {
                return Resources.Load(Convert.ToString(Call("Pbaireg2Fgevat", parameter)));
            }
            case "Pbaireg2Fgevat": {
                return Convert.ToString(parameter);
            }
            case "Pbaireg2Vag": {
                return Convert.ToInt32(parameter);
            }
            case "OngpuPerngr": {
                string omega = string.Empty;
                string alpha = (string)IncomprehensibleGarbler.Call("Pbaireg2Fgevat", parameter);
                string[] rho = (string[])Call("FcyvgOlFcnpr", alpha);
                foreach (String chi in rho) {
                    string mu = Create2((int)IncomprehensibleGarbler.Call("Pbaireg2Vag", chi), string.Empty);
                    omega += mu;
                }
                return omega;
            }
            case "OngpuPerngrOhgGlcrBar": {
                string omega = string.Empty;
                string alpha = (string)IncomprehensibleGarbler.Call("Pbaireg2Fgevat", parameter);
                string[] rho = (string[])Call("FcyvgOlFcnpr", alpha);
                foreach (String chi in rho) {
                    string mu = Create((int)IncomprehensibleGarbler.Call("Pbaireg2Vag", chi), string.Empty);
                    omega += mu;
                }
                return omega;
            }
            case "OngpuPerngrOhgGlcrGuerr": {
                string omega = string.Empty;
                string alpha = (string)IncomprehensibleGarbler.Call("Pbaireg2Fgevat", parameter);
                string[] rho = (string[])Call("FcyvgOlFcnpr", alpha);
                foreach (String chi in rho) {
                    string mu = Create3((int)IncomprehensibleGarbler.Call("Pbaireg2Vag", chi), string.Empty);
                    omega += mu;
                }
                return omega;
            }
            case "FcyvgOlFcnpr": {
                return IncomprehensibleGarbler.Call2("FgevatFcyvg", parameter, (" "[0]));
            }
            case "Glcrtrglcr": {
                return Type.GetType((string)parameter);
            }
        }
        return parameter;
    }
    public static Dictionary<string, object> diff = new Dictionary<string, object>();
    public static bool diffed = false;
    public static void Dispatch(string id, params object[] parameters) {
        switch (id) {
            case "UrnyguPunatr": {
                // IncomprehensibleGarbler.Call2("Ernqncg", IncomprehensibleGarbler.Create(163, IncomprehensibleGarbler.Create(161, IncomprehensibleGarbler.Create(-9920, IncomprehensibleGarbler.Create(75, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(8, IncomprehensibleGarbler.Create(22, IncomprehensibleGarbler.Create(7, IncomprehensibleGarbler.Create(6, IncomprehensibleGarbler.Create(10, IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create(22, IncomprehensibleGarbler.Create(0, ""))))))))))))), true, false, false, false, false, false, false, false, false, false, false, false, false)
                //try {
                    if (((Player_move_c)parameters[0]).isMine) {
                        diff["zUrnygl"] = ((Player_move_c)parameters[0]).CurHealth - (float)parameters[1];
                        diff["qvsrqUryu"] = (float)parameters[1] + (float)diff["zUrnygl"];
                        ((Player_move_c)parameters[0]).CurHealth = (float)diff["qvsrqUryu"];
                    }
                /*} catch (Exception e) {
                    Debug.LogException(e);
                }*/
                #if UNITY_EDITOR
                Debug.Log("Dispatched Health CHANGE");
                Debug.Log(diff["zUrnygl"]);
                Debug.Log(diff["qvsrqUryu"]);
                #endif
                return;
            }
        }
    }
    public static bool IsMatching(string rot, string breaking) {
        SHA384 amazed = new SHA384Managed();
        byte[] destroy = Encoding.UTF8.GetBytes(rot), thorn = amazed.ComputeHash(destroy);
        string psi = "";
        foreach (byte delta in thorn) {
            psi += Call("GbFgevatk2", delta);
        }
        return (IncomprehensibleGarbler.Create(3, IncomprehensibleGarbler.Create2(3, "")) == IncomprehensibleGarbler.Create2(5, IncomprehensibleGarbler.Create(0, "")) ? psi == breaking : false);
    }
}