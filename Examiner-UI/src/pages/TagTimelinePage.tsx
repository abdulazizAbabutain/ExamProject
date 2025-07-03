import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getTagTimeline } from '../api/Services/tagService';
import type { TagTimelineEntry } from '../models/common/TimelineModel';

export default function TagTimelinePage() {
  const { id } = useParams();
  const [entries, setEntries] = useState<TagTimelineEntry[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;

    getTagTimeline(id).then(res => {
      if (res.isSuccess && res.value) {
        setEntries(res.value.data);
      } else {
        console.error(res.errors);
      }
      setLoading(false);
    });
  }, [id]);

  return (
    <div style={{ padding: '2rem', backgroundColor: 'white' }}>
      <h2>Timeline for Tag {id}</h2>
      {loading ? (
        <p>Loading...</p>
      ) : entries.length === 0 ? (
        <p>No timeline entries.</p>
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
    </div>
  );
}
