import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout-footer',
  templateUrl: './layout-footer.component.html',
  styleUrls: ['./layout-footer.component.css']
})
export class LayoutFooterComponent implements OnInit {
  footer = {
    copyrightCertificate: 'eKart Limited',
    copyrightedYear: '2018' };

  constructor() { }

  ngOnInit() {
    console.log(this.footer.copyrightCertificate);
  }

}
