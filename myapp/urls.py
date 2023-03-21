from django.urls import path
from . import views

urlpatterns = [
    path('mymodels/', views.MyModelList.as_view(), name='mymodel_list'),
]
