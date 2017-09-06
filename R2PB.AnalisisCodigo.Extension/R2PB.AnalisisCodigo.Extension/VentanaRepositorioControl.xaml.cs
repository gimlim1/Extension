namespace R2PB.AnalisisCodigo.Extension
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using R2PB.AnalisisCodigo.AC.SolucionesCodigoFuente;
    using RM.Repositorios;
    using System.IO;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Threading;
    using System.ComponentModel;

    /// <summary>
    /// Interaction logic for VentanaRepositorioControl.
    /// </summary>
    public partial class VentanaRepositorioControl : UserControl
    {

        private string url;
        private string workspace;
        private bool fallo;

        public VentanaRepositorioControl()
        {
            this.InitializeComponent();
            fallo = false;
        }

        private void OculteYMuestreModal()
        {
            this.txtUrl.Visibility = Visibility.Hidden;
            this.txtWorkspace.Visibility = Visibility.Hidden;
            this.radioButton.Visibility = Visibility.Hidden;
            this.button.Visibility = Visibility.Hidden;
            this.lblURL.Visibility = Visibility.Hidden;
            this.lblWorkSpace.Visibility = Visibility.Hidden;
            this.circularBox.Visibility = Visibility.Visible;
        }

        private void MuestreItems()
        {
            this.txtUrl.Visibility = Visibility.Visible;
            this.txtWorkspace.Visibility = Visibility.Visible;
            this.radioButton.Visibility = Visibility.Visible;
            this.button.Visibility = Visibility.Visible;
            this.lblURL.Visibility = Visibility.Visible;
            this.lblWorkSpace.Visibility = Visibility.Visible;
            this.circularBox.Visibility = Visibility.Hidden;
        }

        private void CambieMensaje() {
            if (fallo)
            {
                string mensaje = "Ya se realizo el análisis del repositorio.";
                this.txtUrl.Text = mensaje;
                this.txtWorkspace.Text = mensaje;
            }
            else {
                string mensaje = "Se analizo el repositorio con exito.";
                this.txtUrl.Text = mensaje;
                this.txtWorkspace.Text = mensaje;
            } 
        }

        private void Analice(object sender, RoutedEventArgs e)
        {
            OculteYMuestreModal();

            url = this.txtUrl.Text;
            workspace = this.txtWorkspace.Text;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
             
        }
        private void DoWork(object sender, DoWorkEventArgs e)
        {
        
            Consultante elConsultante = new Consultante();
            bool existe = elConsultante.ConsulteSiExisteUltimaVersion(url);

            if (!existe)
            {
                Clonador elClonador = new Clonador();
                elClonador.Clone(url, workspace);

                FileInfo[] archivoClonado = elConsultante.ObtengaLaRutaDeLaSolucion(workspace);
                string rutaClonada = archivoClonado[0].FullName;

                Almacenador elAlmacenador = new Almacenador();
                int idPaquete = elAlmacenador.Almacene(rutaClonada);

                elConsultante.Consulte(idPaquete, url);
                fallo = false;
            }
            else
            {
                fallo = true;
            }
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MuestreItems();
            CambieMensaje();      
        }
    }
}