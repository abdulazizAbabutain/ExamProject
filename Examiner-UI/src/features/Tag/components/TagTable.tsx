import type { TagListModel } from '@/models/tags';
import { useState } from 'react';

type Props = {
  tags: TagListModel[];
  loading: boolean;
  meta?: PagedResponse<Tag>['meta'];
  onFilterChange: (filters: Partial<TagFilterParams>) => void;
};

export default function TagTable({ tags, loading, meta, onFilterChange }: Props) {
  const [localSearch, setLocalSearch] = useState('');
  const [localColorCategory, setLocalColorCategory] = useState('');
  const [localIsArchived, setLocalIsArchived] = useState('');

  const handleSearch = () => {
    onFilterChange({
      search: localSearch,
      colorCategory: localColorCategory,
      isArchived: localIsArchived,
      page: 1,
    });
  };

  const handlePageChange = (newPage: number) => {
    onFilterChange({ page: newPage });
  };

  return (
    <div>
      <div style={{ marginBottom: '1rem' }}>
        <input
          placeholder="Search"
          value={localSearch}
          onChange={(e) => setLocalSearch(e.target.value)}
        />
        <select value={localColorCategory} onChange={(e) => setLocalColorCategory(e.target.value)}>
          <option value="">All Colors</option>
          <option value="Red">Red</option>
          <option value="Green">Green</option>
        </select>
        <select value={localIsArchived} onChange={(e) => setLocalIsArchived(e.target.value)}>
          <option value="">All</option>
          <option value="true">Archived</option>
          <option value="false">Active</option>
        </select>
        <button onClick={handleSearch}>Apply Filters</button>
      </div>

      {loading ? (
        <p>Loading...</p>
      ) : (
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr>
              <th>Name</th>
              <th>Color</th>
              <th>Archived</th>
            </tr>
          </thead>
          <tbody>
            {tags.map((tag) => (
              <tr key={tag.id}>
                <td>{tag.name}</td>
                <td>{tag.colorHexCode}</td>
                <td>{tag.isArchived ? 'Yes' : 'No'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {meta && (
        <div style={{ marginTop: '1rem' }}>
          <button
            disabled={meta.page === 1}
            onClick={() => handlePageChange(meta.page - 1)}
          >
            Prev
          </button>
          <span> Page {meta.page} </span>
          <button
            disabled={tags.length < meta.pageSize}
            onClick={() => handlePageChange(meta.page + 1)}
          >
            Next
          </button>
        </div>
      )}
    </div>
  );
}
