using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.DynamicLinks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DynamicLinkHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    
    private void OnEnable()
    {
        DynamicLinks.DynamicLinkReceived += OnDynamicLinkReceived;
    }

    private void OnDisable()
    {
        DynamicLinks.DynamicLinkReceived -= OnDynamicLinkReceived;
    }

    private void OnDynamicLinkReceived(object sender, EventArgs args)
    {
        ReceivedDynamicLinkEventArgs dynamicLinkEventArgs = args as ReceivedDynamicLinkEventArgs;
        Debug.LogFormat("Received dynamic link {0}",
            dynamicLinkEventArgs.ReceivedDynamicLink.Url.OriginalString);

        string str = $"Original string: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.OriginalString}\n\n" +
                     $"Host: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.Host}\n\n" +
                     $"Port: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.Port}\n\n" +
                     $"Query: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.Query}\n\n" +
                     $"Scheme: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.Scheme}\n\n" +
                     $"Fragment: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.Fragment}\n\n" +
                     $"Segment: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.Segments}\n\n" +
                     $"Authority: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.Authority}\n\n" +
                     $"Authority: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.Authority}\n\n" +
                     $"UserInfo: {dynamicLinkEventArgs.ReceivedDynamicLink.Url.UserInfo}";






        /* Modification by @nasir41 */
        var finalLink = dynamicLinkEventArgs.ReceivedDynamicLink.Url.OriginalString.Replace("https://tryon.page.link/", "");
         finalLink = finalLink.Replace("https://", "");
         finalLink = finalLink.Replace("tryon.page.link/", "");
         finalLink = finalLink.Replace("tryon.page.link", "");

        List<string> linkParamData = finalLink.Split('/').ToList();
        //        foreach (string linkParam in linkParamData)
        //        {
        //            Debug.Log(linkParam);
        //            str += linkParam + " ";
        //        }
        //        str += $"Card Id ={linkParamData.Last()}";
        //        StartCoroutine(SetText(str));
        string SceneName = "";
        if (linkParamData.Count > 0)
        {
            if (linkParamData.Count == 2)
            {
                SceneName = linkParamData[0];
                ApplicationHelper.CARD_ID = linkParamData[1];
            }

            if (linkParamData.Count == 1)
            {
                SceneName = linkParamData[0];
                ApplicationHelper.CARD_ID = "0";
            }
        }
        if (!ApplicationHelper.ALREADY_LINK_OPENED && string.IsNullOrEmpty(SceneName))
        {
            SceneManager.LoadScene(SceneName);//  "MAIN_SCENE" Change by Habib
        }

        /* Modification by @nasir41 */

        //        if (SceneManager.GetActiveScene().name !=(ApplicationHelper.MAIN_SCENE))
        //        {
        //            SceneManager.LoadScene(ApplicationHelper.MAIN_SCENE);
        //        }
        //        else
        //        {
        //            SceneManager.LoadScene()
        //        }

    }

    private IEnumerator SetText(string text)
    {
        tmpText.text = text;
        yield return new WaitForEndOfFrame();

        Debug.Log(tmpText);
    }
}
