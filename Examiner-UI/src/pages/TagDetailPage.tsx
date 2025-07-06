import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getTagById } from "../api/Services/tagService";
import type { TagDetailModel } from "@/models/tags";

export default function TagDetailPage() {
  const { id } = useParams<{ id: string }>();
  const [tag, setTag] = useState<TagDetailModel | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (id) {
      getTagById(id).then((res) => {
        console.log(res)
        if (res.isSuccess) {
          setTag(res.value);
        } else {
          console.error(res.errors);
        }
        setLoading(false);
      });
    }
  }, [id]);

  if (loading) return <p>Loading...</p>;
  if (!tag) return <p>Tag not found.</p>;

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Tag Details</h2>
      <p><strong>Name:</strong> {tag.name}</p>
      <p><strong>Color Hex Code:</strong> {tag.colorHexCode}</p>
      <p><strong>Color Group:</strong> {tag.colorGroup}</p>
      <p><strong>Archived:</strong> {tag.isArchived ? 'Yes' : 'No'}</p>
      <p><strong>Version:</strong> {tag.versionNumber}</p>
      <p><strong>Created At:</strong> {new Date(tag.creationDate).toLocaleString()}</p>
    </div>
  );
}