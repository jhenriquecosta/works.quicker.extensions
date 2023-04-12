using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Quicker.Application.Services.Dto;
using Quicker.Dependency;
using Quicker.Domain.Entities;
using Quicker.Domain.Repositories;
using Quicker.Domain.Uow;
using Quicker.Extensions;
using Quicker.Ui.DevExpressEx.Extensions;
using Quicker.UI.DevExpress.Components.Forms.Contracts;
using Quicker.UI.DevExpress.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExtraGrid = DevExpress.XtraGrid;

namespace Quicker.UI.DevExpress.Components.Forms
{
    public class QuickerForm<Entity,EntityDto> : QuickerForm
    {
        
    }
    public class QuickerForm : XtraForm, IQuickerFormCrud
    {
        protected GridControl _gridViewControl;
      
        public QuickerForm()
        {
            Load += (s, e) => { this.InitData(); };
            
        }
        
       
        
        private Cursor currentCursor;
        protected void RefreshForm(bool b = false)
        {
            if (b)
            {
                currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                Refresh();
            }
            else
                Cursor.Current = currentCursor;
        }
        
        public bool IsNewRecord {get;set; }
        public object ViewModel {get;set;}
        public object DataSource {get;set;}
        public IIocManager Ioc => IocManager.Instance;
        

         public virtual TReturn UsingDb<T,TReturn>(Func<IRepository<T>, TReturn> func) where T : class, IEntity
        {
            TReturn result;

           var repository = IocManager.Instance.Resolve<IRepository<T>>();
            
            using (var uow = IocManager.Instance.Resolve<IUnitOfWorkManager>().Begin())
            {
                result = func(repository);
                uow.Complete();
            }

            return result;
        }
      
        
        public virtual GridControl GetGridView()
        {

             var gridName = "grd" + this.Name;
            var gridControl1 = new DevExtraGrid.GridControl();
            var gridView1 = new DevExtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl1.Location = new System.Drawing.Point(0, 0);
            gridControl1.MainView = gridView1;
            gridControl1.Name = gridName;
            gridControl1.Size = new System.Drawing.Size(1009, 351);
            gridControl1.TabIndex = 0;
            gridControl1.DataSource = DataSource;
           // gridControl1.DoubleClick += OnGrid_DoubleClick;
            gridControl1.ViewCollection.AddRange(new DevExtraGrid.Views.Base.BaseView[] {
            gridView1});
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsFind.FindDelay = 100;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsFind.AlwaysVisible = true;
            gridView1.FocusedRowChanged += new DevExtraGrid.Views.Base.FocusedRowChangedEventHandler(OnGrid_FocusedRowChanged);

            ((System.ComponentModel.ISupportInitialize)(gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(gridView1)).EndInit();
            this.ResumeLayout(false);
            _gridViewControl = gridControl1;
            return gridControl1;
        }
        private void OnGrid_FocusedRowChanged(object sender, DevExtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
             this.ViewModel = (sender as DevExtraGrid.Views.Grid.GridView).GetFocusedRow();
             this.IsNewRecord = false;   
        }
       

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // QuickerForm
            // 
            this.ClientSize = new System.Drawing.Size(383, 136);
            this.Name = "QuickerForm";
            this.ResumeLayout(false);

        }
        public virtual void InitializeServices<TEntity,TEntityDto>() 
            where TEntity : class, IEntity
            where TEntityDto : class, IEntityDto
        {
             // this.Text = nameof(TEntity);             
             this.StartPosition=FormStartPosition.CenterScreen;
            //  InitGridDataSource<TEntity,TEntityDto>();
            var quickerFormCrudService =  QuickerFormCrudService.Instance<TEntity,TEntityDto>(); //  new QuickerFormCrudService<TEntity, TEntityDto>();
            var data  = quickerFormCrudService.GetAll();
            this.DataSource = data;

        }

        public virtual void InitGridDataSource<TEntity,TEntityDto>()
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
        {
             InitDataSource<TEntity,TEntityDto>();
            if (_gridViewControl != null)
            {
                _gridViewControl.DataSource = this.DataSource;
                _gridViewControl.Refresh();
            }            
        }
        public virtual void InitDataSource<TEntity,TEntityDto>()
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
        {
            var result = UsingDb<TEntity,List<TEntityDto>>(r =>
             { 
                 var result = r.GetAllList().AsQueryable().MapTo<TEntityDto>();
                 return result.ToList();
             });
                
            this.DataSource = result;
        }
        public virtual void SaveOrUpdate<TEntity,TEntityDto>()
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
        {
            if (QuickerDevExpressHelper.Pergunta("SALVAR este Registro ?"))
            {

                   var viewModel = ViewModel.ChangeType<TEntityDto>();
                    var dataRecord = viewModel.MapTo<TEntity>();
                
                    UsingDb<TEntity,bool>(r => 
                    {
                        try
                        {
                            if (IsNewRecord)
                                r.Insert(dataRecord);
                            else
                                r.Update(dataRecord);
                            return true;
                        }
                        catch(Exception ex)
                        {
                            QuickerDevExpressHelper.Error($"Erro ao SALVAR o registro {viewModel.Id}{Environment.NewLine}{ex.Message}");
                            return false;
                        }
                    });
                this.IsNewRecord = false;   
                       // InitializeServices(); 
                this.InitGridDataSource<TEntity,TEntityDto>();          
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        public virtual void Delete<TEntity,TEntityDto>()
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
        {
             if (QuickerDevExpressHelper.Pergunta("EXCLUIR este Registro ?")) 
            {
                 var viewModel = ViewModel.ChangeType<TEntityDto>();
                 var dataRecord = viewModel.MapTo<TEntity>();
                 UsingDb<TEntity,bool>(r => 
                    {
                        try
                        {
                            r.Delete(dataRecord);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            QuickerDevExpressHelper.Error($"Erro ao excluir o registro {viewModel.Id}{Environment.NewLine}{ex.Message}");
                            return false;
                        }
                    });
                this.InitGridDataSource<TEntity,TEntityDto>();                           
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        
    }

}
