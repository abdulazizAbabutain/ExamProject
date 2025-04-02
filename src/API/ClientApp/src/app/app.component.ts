import { Component, inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';



export interface Item {
  id: string;
  name: string;
}

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  template: `
    <h1>Items from API</h1>
    <ul>
      <li *ngFor="let item of items">
        {{ item.id }} - {{ item.name }}
      </li>
    </ul>
  `})
export class AppComponent implements OnInit {
  items: Item[] = [];
  
  private http = inject(HttpClient);

  ngOnInit() {
    this.http.get<any[]>('https://localhost:7072/api/Lookup/tag')
      .subscribe({
        next: (data) => this.items = data,
        error: (err) => console.error('API error', err)
      });
  }
  title = 'ClientApp';
}
