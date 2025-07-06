import { useParams } from "react-router-dom";
import { getTagTimelineDetail, type TimelineDetail } from "../api/Services/tagService";
import { useEffect, useState } from "react";


export default function TagTimelineDetailPage() {
  const { tagId, timelineId } = useParams<{ tagId: string; timelineId: string }>();
  const [detail, setDetail] = useState<TimelineDetail | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (tagId && timelineId) {
      getTagTimelineDetail(tagId, timelineId).then((res) => {
        if (res.isSuccess) {
          setDetail(res.value);
        } else {
          console.error(res.errors);
        }
        setLoading(false);
      });
    }
  }, [tagId, timelineId]);

  const renderValue = (val: any) => {
    if (val === undefined || val === null) return <i>None</i>;
    if (Array.isArray(val)) return <ul>{val.map((v, i) => <li key={i}>{renderValue(v)}</li>)}</ul>;
    if (typeof val === 'object') return <pre>{JSON.stringify(val, null, 2)}</pre>;
    return <span>{val.toString()}</span>;
  };

  if (loading) return <p>Loading...</p>;
  if (!detail) return <p>Timeline entry not found.</p>;

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Timeline Detail</h2>
      <p><strong>Action:</strong> {detail.actionType}</p>
      <p><strong>By:</strong> {detail.actionBy}</p>
      <p><strong>Date:</strong> {new Date(detail.timestamp).toLocaleString()}</p>
      <p><strong>Version:</strong> v{detail.versionNumber}</p>

      <h3>Modified Properties</h3>
      {detail.modifiedProperties.length === 0 ? (
        <p>No changes recorded.</p>
      ) : (
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr>
              <th style={thStyle}>Property</th>
              <th style={thStyle}>Old Value</th>
              <th style={thStyle}>New Value</th>
              <th style={thStyle}>Type</th>
            </tr>
          </thead>
          <tbody>
            {detail.modifiedProperties.map((prop, index) => (
              <tr key={index}>
                <td style={tdStyle}>{prop.propertyName}</td>
                <td style={tdStyle}>{renderValue(prop.oldValue)}</td>
                <td style={tdStyle}>{renderValue(prop.newValue)}</td>
                <td style={tdStyle}>{prop.propertyType}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

const thStyle: React.CSSProperties = {
  border: '1px solid #ccc',
  padding: '0.5rem',
  textAlign: 'left',
  background: '#f5f5f5'
};

const tdStyle: React.CSSProperties = {
  border: '1px solid #ddd',
  padding: '0.5rem',
  verticalAlign: 'top'
};
