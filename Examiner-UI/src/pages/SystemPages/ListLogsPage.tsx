import { useEffect, useState } from "react";
import type { LogEntry } from "../../models/logModel";
import { getLogs } from "../../api/Services/logService";


export default function SystemLogsPage() {
  const [logs, setLogs] = useState<LogEntry[]>([]);
  const [page, setPage] = useState(1);
  const [meta, setMeta] = useState<any>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    getLogs(page).then((res) => {
      if (res) {
        setLogs(res.data);
        setMeta(res.metaData);
      }
      setLoading(false);
    });
  }, [page]);

  const renderException = (ex: any, depth = 0) => {
    if (!ex) return null;
    return (
      <div style={{ marginLeft: depth * 20, borderLeft: '2px solid #eee', paddingLeft: 10 }}>
        <strong>{ex.type}</strong>: {ex.message}
        <pre style={{ whiteSpace: 'pre-wrap', background: '#f9f9f9', padding: '0.5rem' }}>{ex.stackTrace}</pre>
        {ex.innerException && renderException(ex.innerException, depth + 1)}
      </div>
    );
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h2>System Logs</h2>

      {loading ? (
        <p>Loading logs...</p>
      ) : logs.length === 0 ? (
        <p>No logs found.</p>
      ) : (
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr>
              <th style={thStyle}>Timestamp</th>
              <th style={thStyle}>Level</th>
              <th style={thStyle}>Message</th>
              <th style={thStyle}>Exception</th>
            </tr>
          </thead>
          <tbody>
            {logs.map((log) => (
              <tr key={log.id}>
                <td style={tdStyle}>{new Date(log.timestamp).toLocaleString()}</td>
                <td style={tdStyle}>{log.level}</td>
                <td style={tdStyle}>{log.message}</td>
                <td style={tdStyle}>{log.exception ? renderException(log.exception) : '-'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      <div style={{ marginTop: '1rem' }}>
        <button onClick={() => setPage((p) => Math.max(p - 1, 1))} disabled={!meta?.hasPreviousPage}>
          Previous
        </button>
        <span style={{ margin: '0 1rem' }}>Page {meta?.pageNumber}</span>
        <button onClick={() => setPage((p) => p + 1)} disabled={!meta?.hasNextPage}>
          Next
        </button>
      </div>
    </div>
  );
}

const thStyle: React.CSSProperties = {
  border: '1px solid #ccc',
  padding: '0.5rem',
  background: '#f0f0f0',
  textAlign: 'left'
};

const tdStyle: React.CSSProperties = {
  border: '1px solid #ddd',
  padding: '0.5rem',
  verticalAlign: 'top'
};
