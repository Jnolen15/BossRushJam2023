using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folder_Cover_Manager : MonoBehaviour
{
    public Moused_Over Folder1;
    public Moused_Over Folder2;
    public Moused_Over Folder3;
    public Moused_Over Folder4;

    // Update is called once per frame
    void Update()
    {
        CheckFolderCall(Folder1);
        CheckFolderCall(Folder2);
        CheckFolderCall(Folder3);
        CheckFolderCall(Folder4);
    }
    private void CheckFolderCall(Moused_Over Folder)
    {
        if (Folder.FolderOpenCall)
        {
            CloseAllFolders(Folder);
            Folder.FolderOpenCall = false;
        }
    }

    private void CloseAllFolders(Moused_Over Folder)
    {
        if (Folder != Folder1) CloseFolder(Folder1);
        if (Folder != Folder2) CloseFolder(Folder2);
        if (Folder != Folder3) CloseFolder(Folder3);
        if (Folder != Folder4) CloseFolder(Folder4);
    }

    private void CloseFolder(Moused_Over Folder)
    {
        Folder.gameObject.SetActive(true);
        Folder.FolderOn = true;
    }

}
