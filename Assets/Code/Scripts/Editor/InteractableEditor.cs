using UnityEditor;
[CustomEditor(typeof(Interactable),true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        base.OnInspectorGUI();
        switch (interactable.useEvents)
        {
            case true:
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                {
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }

                break;
            }
            default:
            {
                if (interactable.GetComponent<InteractionEvent>()!=null)  
                {
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }

                break;
            }
        }
    }
}
