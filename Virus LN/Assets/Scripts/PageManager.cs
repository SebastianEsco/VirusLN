using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageManager : MonoBehaviour
{
    public static PageManager instance;

    public List<FolderTab> pages = new List<FolderTab>();
    public float delayBetweenPages = 0.08f;

    public Collider[] colliders;

    private int currentIndex = -1; // P�gina actualmente seleccionada

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (var col in colliders)
        {
            if(col != null) col.enabled = false;

        }
    }

    public void OnPageSelected(FolderTab selectedPage)
    {
        StopAllCoroutines();
        StartCoroutine(AnimatePages(selectedPage));
        foreach (var col in colliders)
        {
            if(col != null) col.enabled = false;

        }
        if(colliders[selectedPage.index] != null) colliders[selectedPage.index].enabled = true;
    }

    private IEnumerator AnimatePages(FolderTab selectedPage)
    {
        int targetIndex = selectedPage.index;

        // Si es la primera vez, simplemente abrir las correspondientes
        if (currentIndex == -1)
            currentIndex = 0;

        // Si vamos hacia adelante (abrir m�s)
        if (targetIndex > currentIndex)
        {
            for (int i = 0; i <= targetIndex; i++)
            {
                pages[i].SetOpen(true);
                if (i < targetIndex) // sin delay despu�s de la �ltima
                    yield return new WaitForSeconds(delayBetweenPages);
            }

            for (int i = targetIndex + 1; i < pages.Count; i++)
                pages[i].SetOpen(false);
        }
        // Si vamos hacia atr�s (cerrar)
        else if (targetIndex < currentIndex)
        {
            for (int i = currentIndex; i > targetIndex; i--)
            {
                pages[i].SetOpen(false);
                yield return new WaitForSeconds(delayBetweenPages);
            }
        }

        currentIndex = targetIndex;
    }

    public void CloseAll()
    {
        StopAllCoroutines();
        StartCoroutine(CloseAllCoroutine());
    }

    private IEnumerator CloseAllCoroutine()
    {
        for (int i = pages.Count - 1; i >= 0; i--)
        {
            pages[i].SetOpen(false);
            if (i > 0)
                yield return new WaitForSeconds(delayBetweenPages);
        }

        currentIndex = -1;
    }
}
