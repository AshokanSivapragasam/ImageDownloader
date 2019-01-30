import { Component, ViewChild } from '@angular/core';
import { OnInit, AfterViewInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { ThirukkuralService } from '../../services/thirukkural/thiikkural.service';
import { ThirukkuralModel, KuralsModel } from '../../models/thirukkural/thirukkural.models';

@Component({
  selector: 'app-thirukkural',
  templateUrl: './thirukkural.component.html',
  styleUrls: ['./thirukkural.component.css']
})
export class ThirukkuralComponent implements OnInit {
  data: Object;
  title = 'app';

  displayedColumns = ['number', 'section', 'kural', 'chapter'];

  dataSource = new MatTableDataSource<KuralsModel>();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  coreData = [];

  constructor(private dataService: ThirukkuralService,
              private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.loadThirukkurals();
  }

  loadThirukkurals() {
    this.dataService.loadThirukkurals().subscribe(thirukkurals => {
      this.dataSource = new MatTableDataSource<KuralsModel>(thirukkurals.kurals);
      this.dataSource.paginator = this.paginator;
    });
  }
}
