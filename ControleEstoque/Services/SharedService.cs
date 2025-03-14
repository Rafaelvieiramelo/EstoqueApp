using Microsoft.AspNetCore.Components;

namespace LidyDecorApp.Web.Services
{
    public class SharedService
    {
        public void OpenEditModal<T>(ref T modelEdicao, T model, ref bool isEditMode, ref bool showModal)
        {
            modelEdicao = model;
            isEditMode = true;
            showModal = true;
        }

        public void OpenAddModal<T>(ref T modelEdicao, ref bool isEditMode, ref bool showModal) where T : new()
        {
            modelEdicao = new T();
            isEditMode = false;
            showModal = true;
        }

        public static async Task<bool> HideModal(EventCallback hideModalCallback, bool showModal, Action stateHasChanged)
        {
            await hideModalCallback.InvokeAsync(null);
            showModal = false;
            stateHasChanged();
            return showModal;
        }

        public static void ShowAlert(ref bool alertVisible)
        {
            alertVisible = true;
        }

        public static Task CloseAlert(ref string? alertMessage, ref string? alertMessageType, ref bool alertVisible)
        {
            alertMessage = null;
            alertMessageType = null;
            alertVisible = false;
            return Task.CompletedTask;
        }
    }
}
