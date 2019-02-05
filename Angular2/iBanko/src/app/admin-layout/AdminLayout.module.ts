import { CommonModule } from "@angular/common";

@NgModule({
    imports: [
      CommonModule,
      RouterModule.forChild(AdminLayoutRoutes),
      FormsModule,
      MatButtonModule,
      MatRippleModule,
      MatInputModule,
      MatTooltipModule,
    ],
    declarations: [
      DashboardComponent,
      UserProfileComponent,
      TableListComponent,
      TypographyComponent,
      IconsComponent,
      MapsComponent,
      NotificationsComponent,
      UpgradeComponent,
    ]
  })
  
  export class AdminLayoutModule {}
  