import { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import { getTagTimeline } from '../api/Services/tagService';
import type { EntityTimelineModel } from '../common/models/TimelineModel';

export default function TagTimelinePage() {
  const { id } = useParams();
  const [entries, setEntries] = useState<EntityTimelineModel[]>([]);
  const [loading, setLoading] = useState(true);
  const [viewMode, setViewMode] = useState<'table' | 'timeline'>('timeline');
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [hasPreviousPage, setHasPreviousPage] = useState(false);

  useEffect(() => {
    if (!id) return;
    setLoading(true);

    getTagTimeline(id, page, pageSize).then(res => {
      if (res.isSuccess && res.value) {
        setEntries(res.value.data);
        setHasNextPage(res.value.metaData?.hasNextPage || false);
        setHasPreviousPage(res.value.metaData?.hasPreviousPage || false);
      } else {
        console.error(res.errors);
      }
      setLoading(false);
    });
  }, [id, page, pageSize]);

  const handlePageChange = (direction: 'next' | 'prev') => {
    if (direction === 'next' && hasNextPage) setPage(prev => prev + 1);
    else if (direction === 'prev' && hasPreviousPage) setPage(prev => Math.max(prev - 1, 1));
  };

  return (
    <div style={{ padding: '2rem', backgroundColor: 'white' }}>
      <h2>Timeline for Tag {id}</h2>

      <div style={{ marginBottom: '1rem' }}>
        <label>View Mode: </label>
        <select value={viewMode} onChange={(e) => setViewMode(e.target.value as 'table' | 'timeline')}>
          <option value="timeline">Timeline</option>
          <option value="table">Table</option>
        </select>
      </div>

      {loading ? (
        <p>Loading...</p>
      ) : entries.length === 0 ? (
        <p>No timeline entries.</p>
      ) : viewMode === 'table' ? (
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr>
              <th style={thStyle}>#</th>
              <th style={thStyle}>Action</th>
              <th style={thStyle}>By</th>
              <th style={thStyle}>Date</th>
              <th style={thStyle}>Version</th>
              <th style={thStyle}>Comment</th>
              <td style={tdStyle}>Details</td>

            </tr>
          </thead>
          <tbody>
            {entries.map((entry, index) => (
              <tr key={entry.id}>
                <td style={tdStyle}>{(page - 1) * pageSize + index + 1}</td>
                <td style={tdStyle}>{entry.actionType}</td>
                <td style={tdStyle}>{entry.actionBy}</td>
                <td style={tdStyle}>{new Date(entry.timestamp).toLocaleString()}</td>
                <td style={tdStyle}>v{entry.versionNumber}</td>
                <td style={tdStyle}>{entry.comment || '-'}</td>
                <td style={tdStyle}>
                  <Link to={`/tags/${id}/timeline/${entry.id}`}>
                    {entry.actionType}
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <ul>
          {entries.map((entry) => (
            <li key={entry.id} style={{ marginBottom: '1rem' }}>
              <strong>{entry.actionType}</strong> by {entry.actionBy} on{' '}
              {new Date(entry.timestamp).toLocaleString()} (v{entry.versionNumber})
              {entry.comment && <div><em>Comment:</em> {entry.comment}</div>}
            </li>
          ))}
        </ul>
      )}

      <div style={{ marginTop: '1rem' }}>
        <button onClick={() => handlePageChange('prev')} disabled={!hasPreviousPage}>Previous</button>
        <span style={{ margin: '0 1rem' }}>Page {page}</span>
        <button onClick={() => handlePageChange('next')} disabled={!hasNextPage}>Next</button>
      </div>
    </div>
  );
}

const thStyle: React.CSSProperties = {
  border: '1px solid #ccc',
  padding: '0.5rem',
  textAlign: 'left'
};

const tdStyle: React.CSSProperties = {
  border: '1px solid #ddd',
  padding: '0.5rem'
};
